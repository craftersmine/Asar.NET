using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace craftersmine.Asar.Net
{
    public class AsarUnpackingStatusChangedEventArgs : EventArgs
    {
        public int TotalFiles { get; private set; }
        public int CurrentFile { get; private set; }
        public string OutputFilePath { get; private set; }
        public AsarArchiveFile CurrentFileData { get; private set; }
        //public AsarUnpackingStatus Status { get; private set; }

        internal AsarUnpackingStatusChangedEventArgs(int totalFiles, int currentFile, string outputFilePath, AsarArchiveFile currentFileData)
        {
            TotalFiles = totalFiles;
            CurrentFile = currentFile;
            OutputFilePath = outputFilePath;
            CurrentFileData = currentFileData;
        }
    }

    public class AsarUnpackingCompletedEventArgs : EventArgs
    {
        public string OutputDirectoryPath { get; private set; }
        public DirectoryInfo OutputDirectory { get; private set; }

        internal AsarUnpackingCompletedEventArgs(string outputDirectoryPath, DirectoryInfo outputDirectory)
        {
            OutputDirectoryPath = outputDirectoryPath;
            OutputDirectory = outputDirectory;
        }
    }

    //public enum AsarUnpackingStatus
    //{

    //}
}
