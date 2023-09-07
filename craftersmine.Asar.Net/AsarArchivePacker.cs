using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an ASAR archive packer. This class cannot be inherited
    /// </summary>
    public sealed class AsarArchivePacker
    {
        private FileStream archiveStream;
        private long currentArchiveSize = 0;
        private int archiveHeaderSize = 0;
        private int totalFileCount = 0;
        private int currentFileProcessing = 0;
        private byte[] archiveHeader;
        private string outputFilePath = string.Empty;
        private string outputUnpackedPath = string.Empty;

        /// <summary>
        /// Gets <see langword="true"/> if packing process is paused
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Gets <see langword="true"/> if packer is currently packing archive
        /// </summary>
        public bool IsPacking { get; private set; }

        /// <summary>
        /// Gets current ASAR archive data for packer
        /// </summary>
        public AsarArchivePackerData PackerData { get; private set; }

        /// <summary>
        /// Occurs when packing status is changed
        /// </summary>
        public event EventHandler<AsarPackingEventArgs> StatusChanged;
        /// <summary>
        /// Occurs when ASAR archive is packed successfully
        /// </summary>
        public event EventHandler<AsarPackingCompletedEventArgs> AsarArchivePacked; 

        /// <summary>
        /// Creates ASAR archive packer from specified data
        /// </summary>
        /// <param name="packerData">ASAR archive data for packer</param>
        /// <exception cref="ArgumentNullException">When packer data is null</exception>
        public AsarArchivePacker(AsarArchivePackerData packerData)
        {
            if (packerData is null)
                throw new ArgumentNullException(nameof(packerData));

            PackerData = packerData;
        }
        
        /// <summary>
        /// Packs ASAR archive with specified info
        /// </summary>
        public async Task PackAsync()
        {
            await PackAsync(CancellationToken.None);
        }

        /// <summary>
        /// Packs ASAR archive with specified info
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation</param>
        public async Task PackAsync(CancellationToken cancellationToken)
        {
            // Header size integer                                            | Header metadata size  
            // int                                 | int                      | int                                                    | int
            // size of whole header size (4 bytes) | size of header with size | size of header metadata (JSON data) with string length | size of header metadata (JSON data) 

            // build output file path
            outputFilePath = Path.Combine(PackerData.OutputDirectoryPath, PackerData.ArchiveName + ".asar");
            outputUnpackedPath = outputFilePath + ".unpacked";
            StatusChanged?.Invoke(this, new AsarPackingEventArgs(totalFileCount, currentFileProcessing, outputFilePath, AsarPackingStatus.CreatingHeader, null));

            // create archive file structure
            AsarArchiveFile archiveHeaderData = await BuildArchiveHeaderAsync();
            archiveHeaderData.IsRoot = true;
            PopulateParents(archiveHeaderData);

            totalFileCount = archiveHeaderData.GetFileCount();

            // sort files in archive if it is required
            if (PackerData.PerformSort)
            {
                StatusChanged?.Invoke(this, new AsarPackingEventArgs(totalFileCount, currentFileProcessing, outputFilePath, AsarPackingStatus.Sorting, null));
                SortFiles(archiveHeaderData);
            }

            // serialize header into json and get byte array from string
            StatusChanged?.Invoke(this, new AsarPackingEventArgs(totalFileCount, currentFileProcessing, outputFilePath, AsarPackingStatus.SerializingHeader, null));
            string archiveHeaderJson = AsarArchiveFileJsonSerialezer.Serialeze(archiveHeaderData);
            this.archiveHeader = Encoding.UTF8.GetBytes(archiveHeaderJson);
            archiveHeaderSize = this.archiveHeader.Length;

            if (!Directory.Exists(PackerData.OutputDirectoryPath))
                Directory.CreateDirectory(PackerData.OutputDirectoryPath);

            // create output file and open file stream
            archiveStream = File.Create(outputFilePath);
            
            // write asar header to file stream
            StatusChanged?.Invoke(this, new AsarPackingEventArgs(totalFileCount, currentFileProcessing, outputFilePath, AsarPackingStatus.WritingHeader, null));
            await Task.Run(() =>
            {
                BinaryWriter headerWriter = new BinaryWriter(archiveStream);
                headerWriter.Write(4);
                headerWriter.Write(archiveHeaderSize + 8);
                headerWriter.Write(archiveHeaderSize + 4);
                headerWriter.Write(archiveHeaderSize);
            });
            await archiveStream.WriteAsync(this.archiveHeader, 0, archiveHeaderSize, cancellationToken);

            // start writing files to asar recursively
            await WriteFileToArchive(archiveHeaderData, cancellationToken);

            // close when all files are writen
            archiveStream.Close();
            StatusChanged?.Invoke(this, new AsarPackingEventArgs(totalFileCount, currentFileProcessing, outputFilePath, AsarPackingStatus.Completed, null));

            // if we subscribed to OnAsarArchivePacked
            if (!(AsarArchivePacked is null))
            {
                // open created asar archive and invoke events
                AsarArchive arc = new AsarArchive(outputFilePath);
                AsarArchivePacked?.Invoke(this, new AsarPackingCompletedEventArgs(outputFilePath, arc));
            }
        }

        private void SortFiles(AsarArchiveFile file)
        {
            if (!(file.Files is null) && file.Files.Any())
            {
                foreach (KeyValuePair<string, AsarArchiveFile> f in file.Files)
                {
                    SortFiles(f.Value);
                }

                file.Files = new SortedDictionary<string, AsarArchiveFile>(file.Files).ToAsarArchiveFileCollection();
            }
        }

        /// <summary>
        /// Pauses packing process
        /// </summary>
        public void Pause()
        {
            if (IsPacking)
                IsPaused = true;
        }

        /// <summary>
        /// Resumes packing process
        /// </summary>
        public void Resume()
        {
            if (IsPaused && IsPacking)
                IsPaused = false;
        }

        private async Task WriteFileToArchive(AsarArchiveFile file, CancellationToken cancellationToken)
        {
            while (IsPaused)
            {
                try
                {
                    await Task.Delay(500, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    IsPaused = false;
                }
            }

            // check if "file" is a directory
            if (!(file.Files is null) && file.Files.Any())
            {
                // if it is and has files, write all files that this folder has in asar archive
                foreach (AsarArchiveFile f in file.Files.Values)
                {
                    await WriteFileToArchive(f, cancellationToken);
                }
            }
            
            currentFileProcessing++;
            StatusChanged?.Invoke(this, new AsarPackingEventArgs(totalFileCount, currentFileProcessing, outputFilePath, AsarPackingStatus.Packing, file));
            // if it is file
            if (file.IsFile)
            {
                // if it is packed
                if (!file.IsUnpacked)
                {
                    // open file that is currently being packed and copy stream contents into asar archive stream
                    using (FileStream fileStream = File.OpenRead(file.FilePath))
                    {
                        await fileStream.CopyToAsync(archiveStream, 81920, cancellationToken);
                    }
                }
                // if it is unpacked
                else
                {
                    // create asar.unpacked dir
                    if (!Directory.Exists(outputUnpackedPath))
                        Directory.CreateDirectory(outputUnpackedPath);

                    // get file info of file that being packed
                    FileInfo fileInfo = new FileInfo(file.FilePath);
                    string destFileName = Path.Combine(outputUnpackedPath, file.GetPathInArchive());
                    // create dir at file path if it is not existent
                    string fileDir = Path.GetDirectoryName(destFileName);
                    if (!(fileDir is null) && !Directory.Exists(fileDir))
                        Directory.CreateDirectory(fileDir);

                    // copy file to asar.unpacked at specified location
                    fileInfo.CopyTo(destFileName);
                }
            }
        }
        
        private void PopulateParents(AsarArchiveFile file)
        {
            // recursively populate parents
            foreach (KeyValuePair<string, AsarArchiveFile> f in file.Files)
            {
                f.Value.Parent = file;
                f.Value.Name = f.Key;
                if (!(f.Value.Files is null) && f.Value.Files.Any())
                    PopulateParents(f.Value);
            }
        }

        private async Task<AsarArchiveFile> BuildArchiveHeaderAsync()
        {
            // create asar archive file root from pending files and directories
            AsarArchiveFile root = new AsarArchiveFile();

            foreach (AsarArchivePendingFile file in PackerData.PendingFiles)
            {
                AsarArchiveFileIntegrity integrity = null;
                if (file.CalculateIntegrity)
                    integrity = await AsarArchiveFileIntegrity.GetFileIntegrityAsync(file.FilePath);

                AsarArchiveFile asarArchiveFile = new AsarArchiveFile(currentArchiveSize, file.FileInfo.Length,
                        file.IsUnpacked,
                        IsExecutable(file.FileInfo), file.FileInfo.Name, integrity);
                asarArchiveFile.IsFile = true;
                asarArchiveFile.FilePath = file.FilePath;
                asarArchiveFile.Files = null;

                if (!file.IsUnpacked)
                    currentArchiveSize += file.FileInfo.Length;

                root.Files.Add(file.FileInfo.Name, asarArchiveFile);
            }

            foreach (AsarArchivePendingDirectory dir in PackerData.PendingDirectories)
            {
                AsarArchiveFile d = await ProcessDirectoryAsync(dir);
                root.Files.Add(d.Name, d);
            }

            return root;
        }

        private async Task<AsarArchiveFile> ProcessDirectoryAsync(AsarArchivePendingDirectory dir)
        {
            AsarArchiveFile processedDir = new AsarArchiveFile();
            processedDir.Name = dir.DirectoryInfo.Name;

            foreach (AsarArchivePendingFile file in dir.PendingFiles)
            {
                AsarArchiveFileIntegrity integrity = null;
                if (file.CalculateIntegrity)
                    integrity = await AsarArchiveFileIntegrity.GetFileIntegrityAsync(file.FilePath);

                processedDir.Files.Add(file.FileInfo.Name,
                    new AsarArchiveFile(currentArchiveSize, file.FileInfo.Length, file.IsUnpacked,
                        IsExecutable(file.FileInfo), file.FileInfo.Name, integrity) { Files = null, IsFile = true, FilePath = file.FilePath });
                currentArchiveSize += file.FileInfo.Length;
            }

            foreach (AsarArchivePendingDirectory directory in dir.PendingDirectories)
            {
                AsarArchiveFile d = await ProcessDirectoryAsync(directory);
                processedDir.Files.Add(d.Name, d);
            }

            return processedDir;
        }

        private bool IsExecutable(FileInfo file)
        {
            // check if file is executable (just for Unix platforms since windows determines executables by extension)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Path.GetExtension(file.FullName) == ".exe";
            }

            // TODO: implement checking for executable on Unix platforms

            return false;
        }
    }
}
