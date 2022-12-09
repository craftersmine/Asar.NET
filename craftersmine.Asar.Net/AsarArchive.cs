using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an Electron ASAR archive
    /// </summary>
    public partial class AsarArchive : IDisposable
    {
        // represents size of header whole size of ASAR header integer,
        // but since it is 4 bytes long and integer next to it has size of 4,
        // first 4 bytes will be (most of the time and until something changes) 04 00 00 00,
        // so we can assume that it is ASAR based on these bytes
        private static readonly byte[] Signature = new byte[] {0x04, 0x00, 0x00, 0x00};

        private int headerSize;
        private int headerMetadataSize;
        private int filesOffset;
        private byte[] headerData;
        private Stream archiveStream;
        private BinaryReader archiveBinaryReader;

        /// <summary>
        /// Gets a size of the header metadata
        /// </summary>
        public long HeaderSize => headerMetadataSize;

        /// <summary>
        /// Gets an offset value where in the archive actual archived files data starts
        /// </summary>
        public int FilesDataOffset => filesOffset;

        /// <summary>
        /// Gets a <see langword="true"/> if archive loaded from file, otherwise <see langword="false"/>
        /// </summary>
        public bool IsFile { get; private set; }

        /// <summary>
        /// Gets a path to archive file or empty string if archive was loaded from file
        /// </summary>
        public string FilePath { get; private set; } = string.Empty;

        /// <summary>
        /// Gets a path to unpacked files of this ASAR archive
        /// </summary>
        public string UnpackedFilesPath
        {
            get
            {
                if (!IsFile || string.IsNullOrWhiteSpace(FilePath))
                    return string.Empty;

                string path = Path.Combine(Path.GetDirectoryName(FilePath),
                    Path.GetFileNameWithoutExtension(FilePath) + ".asar.unpacked");
                return path;
            }
        }

        /// <summary>
        /// Gets packed files metadata, such as file sizes, offsets, integrity data, etc.
        /// </summary>
        public AsarArchiveFile Files { get; private set; }

        /// <summary>
        /// Opens an ASAR archive from data stream
        /// </summary>
        /// <param name="stream">Data stream with ASAR archive data</param>
        /// <exception cref="AsarException">When data in the stream is not ASAR archive</exception>
        public AsarArchive(Stream stream)
        {
            archiveStream = stream;
            archiveBinaryReader = new BinaryReader(archiveStream);

            byte[] sig = archiveBinaryReader.ReadBytes(4);
            ReadOnlySpan<byte> sigSpan = new ReadOnlySpan<byte>(sig);
            if (!sigSpan.SequenceEqual(Signature))
                throw new AsarException("Not an ASAR archive in file/stream");

            headerSize = (int)archiveBinaryReader.ReadUInt32();
            archiveStream.Seek(4, SeekOrigin.Current);

            headerMetadataSize = (int)archiveBinaryReader.ReadUInt32();
            headerData = archiveBinaryReader.ReadBytes(headerMetadataSize);
            string header = Encoding.UTF8.GetString(headerData);

            filesOffset = headerSize + 8;
            Files = JsonConvert.DeserializeObject<AsarArchiveFile>(header);
            Files.IsRoot = true;
            PopulateParents(Files);
        }

        /// <summary>
        /// Opens an ASAR archive from file
        /// </summary>
        /// <param name="filePath">Path to ASAR archive</param>
        /// <exception cref="AsarException">When data in the file is not ASAR archive</exception>
        public AsarArchive(string filePath) : this(File.OpenRead(filePath))
        {
            IsFile = true;
            FilePath = filePath;
        }

        /// <summary>
        /// Closes an ASAR archive, closes file stream and disposes all used resources
        /// </summary>
        public void Dispose()
        {
            archiveBinaryReader.Dispose();
            archiveStream?.Dispose();
        }

        /// <summary>
        /// Reads all bytes of ASAR archived file byt specified path within archive into <see cref="byte"/> array
        /// </summary>
        /// <param name="path">Path to file within ASAR archive</param>
        /// <returns>An array of <see cref="byte"/> from file</returns>
        public async Task<byte[]> ReadBytesAsync(string path)
        {
            return await ReadBytesAsync(path, CancellationToken.None);
        }

        /// <summary>
        /// Reads all bytes of ASAR archived file byt specified path within archive into <see cref="byte"/> array
        /// </summary>
        /// <param name="path">Path to file within ASAR archive</param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>An array of <see cref="byte"/> from file</returns>
        public async Task<byte[]> ReadBytesAsync(string path, CancellationToken cancellationToken)
        {
            AsarArchiveFile file = FindFile(path);
            return await ReadBytesAsync(file, cancellationToken);
        }

        /// <summary>
        /// Reads all bytes of ASAR archived file into <see cref="byte"/> array
        /// </summary>
        /// <param name="file"></param>
        /// <returns>An array of <see cref="byte"/> from file</returns>
        public async Task<byte[]> ReadBytesAsync(AsarArchiveFile file)
        {
            return await ReadBytesAsync(file, CancellationToken.None);
        }

        /// <summary>
        /// Reads all bytes of ASAR archived file into <see cref="byte"/> array
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns>An array of <see cref="byte"/> from file</returns>
        public async Task<byte[]> ReadBytesAsync(AsarArchiveFile file, CancellationToken cancellationToken)
        {
            byte[] data = new byte[file.Size];

            if (file.IsUnpacked)
            {
                return await ReadUnpackedBytesAsync(file);
            }

            archiveStream.Seek(filesOffset + (int)file.Offset, SeekOrigin.Begin);
            int read = await archiveStream.ReadAsync(data, 0, (int)file.Size);

            return data;
        }

        /// <summary>
        /// Opens a file with specified location within archive as stream
        /// </summary>
        /// <param name="path">Path to file within archive</param>
        /// <returns><see cref="AsarFileStream"/> if file is packed or <see cref="FileStream"/> if file is unpacked</returns>
        public Stream OpenFileAsStream(string path)
        {
            AsarArchiveFile file = FindFile(path);
            return OpenFileAsStream(file);
        }

        /// <summary>
        /// Opens a file that located in archive as stream
        /// </summary>
        /// <param name="file">File to read as stream</param>
        /// <returns><see cref="AsarFileStream"/> if file is packed or <see cref="FileStream"/> if file is unpacked</returns>
        public Stream OpenFileAsStream(AsarArchiveFile file)
        {
            if (file.IsUnpacked)
                return OpenUnpackedAsStream(file);

            AsarFileStream asarFileStream =
                new AsarFileStream(archiveStream, filesOffset, (int) file.Offset, (int) file.Size);
            return asarFileStream;
        }

        /// <summary>
        /// Gets an ASAR archived file by specified path in archive
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AsarArchiveFile FindFile(string path)
        {
            if (path.StartsWith("\\") || path.StartsWith(Path.DirectorySeparatorChar.ToString()) || path.StartsWith(Path.AltDirectorySeparatorChar.ToString()))
                path = path.Substring(1);
            if (path.EndsWith("\\") || path.EndsWith(Path.DirectorySeparatorChar.ToString()) || path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                path = path.Remove(path.Length - 1);

            string[] dirs = Path.GetDirectoryName(path)?.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            if (dirs is null || dirs.Length == 0) return Files.Files.FirstOrDefault(f => f.Key == path).Value;

            AsarArchiveFile lastDir = Files;
            foreach (var dir in dirs)
            {
                lastDir = lastDir.Files[dir];
            }

            if (!lastDir.Files.ContainsKey(Path.GetFileName(path)))
                throw new FileNotFoundException("File is not found in ASAR archive", path);

            return lastDir.Files[Path.GetFileName(path)];
        }
    }
}
