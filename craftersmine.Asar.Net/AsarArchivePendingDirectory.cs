using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents a pending directory to be packed in ASAR archive
    /// </summary>
    public class AsarArchivePendingDirectory
    {
        /// <summary>
        /// Gets full path to directory which needs to be archived
        /// </summary>
        public string DirectoryPath => DirectoryInfo.FullName;

        /// <summary>
        /// Gets or sets directory info which needs to be archived
        /// </summary>
        public DirectoryInfo DirectoryInfo { get; set; }

        public AsarArchivePendingFile[] PendingFiles { get; private set; }
        public AsarArchivePendingDirectory[] PendingDirectories { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="AsarArchivePendingFile"/>
        /// </summary>
        /// <param name="dirInfo">File information about packing file</param>
        public AsarArchivePendingDirectory(DirectoryInfo dirInfo, bool unpacked, bool calculateIntegrity)
        {
            if (!dirInfo.Exists)
                throw new FileNotFoundException("File not found at specified location", dirInfo.Name);
            DirectoryInfo = dirInfo;

            List<AsarArchivePendingFile> pendingFiles = new List<AsarArchivePendingFile>();

            foreach (FileInfo file in dirInfo.EnumerateFiles())
            {
                pendingFiles.Add(new AsarArchivePendingFile(file) {CalculateIntegrity = calculateIntegrity, IsUnpacked = unpacked});
            }

            List<AsarArchivePendingDirectory> pendingDirectories = new List<AsarArchivePendingDirectory>();

            foreach (DirectoryInfo dir in dirInfo.EnumerateDirectories())
            {
                pendingDirectories.Add(new AsarArchivePendingDirectory(dir, unpacked, calculateIntegrity));
            }

            PendingDirectories = pendingDirectories.ToArray();
            PendingFiles = pendingFiles.ToArray();
        }
    }
}
