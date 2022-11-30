using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using FileInfo = System.IO.FileInfo;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an ASAR archive builder
    /// </summary>
    public class AsarArchivePackerData
    {
        internal List<AsarArchivePendingFile> PendingFilesInternal { get; set; }
        internal List<AsarArchivePendingDirectory> PendingDirectoriesInternal { get; set; }

        /// <summary>
        /// Gets currently pending files for adding into new archive
        /// </summary>
        public AsarArchivePendingFile[] PendingFiles => PendingFilesInternal.ToArray();

        public AsarArchivePendingDirectory[] PendingDirectories => PendingDirectoriesInternal.ToArray();

        /// <summary>
        /// Gets an output directory path for archive
        /// </summary>
        public string OutputDirectoryPath { get; private set; }

        /// <summary>
        /// Gets an ASAR archive name
        /// </summary>
        public string ArchiveName { get; private set; }

        public bool PerformSort { get; set; }

        /// <summary>
        /// Creates a new instance of ASAR archive data
        /// </summary>
        /// <param name="outputDir"></param>
        /// <param name="archiveName"></param>
        public AsarArchivePackerData(string outputDir, string archiveName)
        {
            OutputDirectoryPath = outputDir;
            ArchiveName = archiveName;
            PendingFilesInternal = new List<AsarArchivePendingFile>();
            PendingDirectoriesInternal = new List<AsarArchivePendingDirectory>();
        }
    }
}
