using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents ASAR archive unpacker. This class cannot be inherited
    /// </summary>
    public sealed class AsarArchiveUnpacker
    {
        private int currentFileIndex = 0;
        private string outputDir = string.Empty;

        /// <summary>
        /// Gets an ASAR archive that associated with this unpacker
        /// </summary>
        public AsarArchive Archive { get; private set; }

        /// <summary>
        /// Occurs when unpacking using <see cref="UnpackAsync"/> status changed
        /// </summary>
        public event EventHandler<AsarUnpackingStatusChangedEventArgs> StatusChanged;
        /// <summary>
        /// Occurs when ASAR archive fully unpacked by using <see cref="UnpackAsync"/>
        /// </summary>
        public event EventHandler<AsarUnpackingCompletedEventArgs> AsarArchiveUnpacked;

        /// <summary>
        /// Creates new instance of ASAR archive unpacker
        /// </summary>
        /// <param name="archive"><see cref="AsarArchive"/> that needs to be unpacked</param>
        public AsarArchiveUnpacker(AsarArchive archive)
        {
            Archive = archive;
        }

        /// <summary>
        /// Unpacks specified ASAR <see cref="Archive"/> into specified output directory
        /// </summary>
        /// <param name="outputDir">Path to the output directory</param>
        public async Task UnpackAsync(string outputDir)
        {
            this.outputDir = outputDir;

            await UnpackFileAsyncInternal(Archive.Files, CancellationToken.None);

            AsarArchiveUnpacked?.Invoke(this, new AsarUnpackingCompletedEventArgs(new DirectoryInfo(this.outputDir)));
        }

        /// <summary>
        /// Unpacks specified ASAR <see cref="Archive"/> into specified output directory
        /// </summary>
        /// <param name="outputDir">Path to the output directory</param>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        /// <returns></returns>
        public async Task UnpackAsync(string outputDir, CancellationToken cancellationToken)
        {
            this.outputDir = outputDir;

            await UnpackFileAsyncInternal(Archive.Files, cancellationToken);

            AsarArchiveUnpacked?.Invoke(this, new AsarUnpackingCompletedEventArgs(new DirectoryInfo(this.outputDir)));
        }

        /// <summary>
        /// Unpacks file with specified path in archive to output file path
        /// </summary>
        /// <param name="pathInArchive">File path within ASAR archive</param>
        /// <param name="outputFilePath">Path to the output file</param>
        /// <exception cref="ArgumentNullException">When file path in archive or output file path is null or empty</exception>
        public async Task UnpackFileAsync(string pathInArchive, string outputFilePath)
        {
            if (string.IsNullOrWhiteSpace(pathInArchive))
                throw new ArgumentNullException(nameof(pathInArchive));
            if (string.IsNullOrWhiteSpace(outputFilePath))
                throw new ArgumentNullException(nameof(outputFilePath));
            
            string dir = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (FileStream fileStream = File.Create(outputFilePath))
            {
                await UnpackFileAsync(pathInArchive, fileStream);
            }
        }

        /// <summary>
        /// Unpacks file with specified path in archive to specified stream
        /// </summary>
        /// <param name="pathInArchive">File path within ASAR archive</param>
        /// <param name="outputStream">Data stream to which unpack specified file</param>
        /// <exception cref="ArgumentNullException">When file path in archive is null or empty or output stream is null</exception>
        public async Task UnpackFileAsync(string pathInArchive, Stream outputStream)
        {
            if (string.IsNullOrWhiteSpace(pathInArchive))
                throw new ArgumentNullException(nameof(pathInArchive));

            await UnpackFileAsync(Archive.FindFile(pathInArchive), outputStream);
        }

        /// <summary>
        /// Unpacks specified file in archive to output file path
        /// </summary>
        /// <param name="file">File within ASAR archive</param>
        /// <param name="outputFilePath">Path to the output file</param>
        /// <exception cref="ArgumentNullException">When specified file within ASAR archive is null or output file path is null or empty</exception>
        public async Task UnpackFileAsync(AsarArchiveFile file, string outputFilePath)
        {
            if (string.IsNullOrWhiteSpace(outputFilePath))
                throw new ArgumentNullException(nameof(outputFilePath));

            string dir = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (FileStream fileStream = File.Create(outputFilePath))
            {
                await UnpackFileAsync(file, fileStream);
            }
        }

        /// <summary>
        /// Unpacks specified file in archive to output stream
        /// </summary>
        /// <param name="file">File within ASAR archive</param>
        /// <param name="outputStream">Data stream to which unpack specified file</param>
        /// <exception cref="ArgumentNullException">When specified file withing ASAR archive is null or output stream is null</exception>
        /// <exception cref="ArgumentException">When output stream for unpacked file is read-only</exception>
        public async Task UnpackFileAsync(AsarArchiveFile file, Stream outputStream)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (outputStream is null)
                throw new ArgumentNullException(nameof(outputStream));

            if (!outputStream.CanWrite)
                throw new ArgumentException("Output stream for unpacked file is read-only!", nameof(outputStream));

            if (file.IsUnpacked)
            {
                using (Stream fileStream = Archive.OpenFileAsStream(file))
                {
                    await fileStream.CopyToAsync(outputStream);
                }
            }
            else
            {
                using (AsarFileStream fileStream = Archive.OpenFileAsStream(file) as AsarFileStream)
                {
                    await fileStream.CopyToAsync(outputStream);
                }
            }
        }

        private async Task UnpackFileAsyncInternal(AsarArchiveFile file, CancellationToken cancellationToken)
        {
            currentFileIndex++;
            string filePath = Path.Combine(outputDir, file.GetPathInArchive());
            StatusChanged?.Invoke(this, new AsarUnpackingStatusChangedEventArgs(Archive.Files.GetFileCount(), currentFileIndex, filePath, file));
            if (!(file.Files is null) && file.Files.Any())
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                foreach (AsarArchiveFile f in file.Files.Values)
                {
                    await UnpackFileAsyncInternal(f, cancellationToken);
                }
            }
            else
            {
                if (!file.IsUnpacked)
                {
                    using (AsarFileStream asarFileStream = Archive.OpenFileAsStream(file) as AsarFileStream)
                    {
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            await asarFileStream?.CopyToAsync(fileStream, 81920, cancellationToken);
                        }
                    }
                }
                else
                {
                    using (Stream asarFileStream = Archive.OpenFileAsStream(file))
                    {
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            await asarFileStream.CopyToAsync(fileStream, 81920, cancellationToken);
                        }
                    }
                }
            }
        }
    }
}
