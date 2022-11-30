using System;
using System.Collections.Generic;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an ASAR packing event args
    /// </summary>
    public class AsarPackingEventArgs : EventArgs
    {
        /// <summary>
        /// Gets total amount of files to pack
        /// </summary>
        public int TotalFiles { get; private set; }

        /// <summary>
        /// Gets current packing file index
        /// </summary>
        public int CurrentFile { get; private set; }

        /// <summary>
        /// Gets current packing file path
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets current packing status
        /// </summary>
        public AsarPackingStatus PackingStatus { get; private set; }

        /// <summary>
        /// Gets current processing file data
        /// </summary>
        public AsarArchiveFile CurrentFileData { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalFiles"></param>
        /// <param name="currentFile"></param>
        /// <param name="filePath"></param>
        /// <param name="status"></param>
        /// <param name="archiveFile"></param>
        public AsarPackingEventArgs(int totalFiles, int currentFile, string filePath, AsarPackingStatus status, AsarArchiveFile archiveFile)
        {
            TotalFiles = totalFiles;
            CurrentFile = currentFile;
            FilePath = filePath;
            PackingStatus = status;
            CurrentFileData = archiveFile;
        }
    }

    /// <summary>
    /// Represents an ASAR packing completed event args
    /// </summary>
    public class AsarPackingCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a path to packed ASAR archive
        /// </summary>
        public string AsarFilePath { get; private set; }
        /// <summary>
        /// Gets an instance of a packed ASAR archive
        /// </summary>
        public AsarArchive PackedArchive { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asarFilePath"></param>
        /// <param name="packedArchive"></param>
        public AsarPackingCompletedEventArgs(string asarFilePath, AsarArchive packedArchive)
        {
            AsarFilePath = asarFilePath;
            PackedArchive = packedArchive;
        }
    }

    /// <summary>
    /// Represents an ASAR packing status
    /// </summary>
    public enum AsarPackingStatus
    {
        /// <summary>
        /// Creating header
        /// </summary>
        CreatingHeader,
        /// <summary>
        /// Sorting files
        /// </summary>
        Sorting,
        /// <summary>
        /// Serializing header
        /// </summary>
        SerializingHeader,
        /// <summary>
        /// Writing header
        /// </summary>
        WritingHeader,
        /// <summary>
        /// Packing files
        /// </summary>
        Packing,
        /// <summary>
        /// Packing completed
        /// </summary>
        Completed
    }
}
