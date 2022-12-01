using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Contains <see cref="AsarArchiveUnpacker.StatusChanged"/> event arguments
    /// </summary>
    public class AsarUnpackingStatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets total amount of files to be unpacked
        /// </summary>
        public int TotalFiles { get; private set; }
        /// <summary>
        /// Gets current file index that is unpacking
        /// </summary>
        public int CurrentFile { get; private set; }
        /// <summary>
        /// Gets current file output path
        /// </summary>
        public string OutputFilePath { get; private set; }
        /// <summary>
        /// Gets current file data within ASAR archive
        /// </summary>
        public AsarArchiveFile CurrentFileData { get; private set; }

        internal AsarUnpackingStatusChangedEventArgs(int totalFiles, int currentFile, string outputFilePath, AsarArchiveFile currentFileData)
        {
            TotalFiles = totalFiles;
            CurrentFile = currentFile;
            OutputFilePath = outputFilePath;
            CurrentFileData = currentFileData;
        }
    }
    
    /// <summary>
    /// Contains <see cref="AsarArchiveUnpacker.AsarArchiveUnpacked"/> event arguments
    /// </summary>
    public class AsarUnpackingCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets output directory of unpacked archive
        /// </summary>
        public string OutputDirectoryPath => OutputDirectory.FullName;
        /// <summary>
        /// Gets information about output directory of unpacked archive
        /// </summary>
        public DirectoryInfo OutputDirectory { get; private set; }

        internal AsarUnpackingCompletedEventArgs(DirectoryInfo outputDirectory)
        {
            OutputDirectory = outputDirectory;
        }
    }
}
