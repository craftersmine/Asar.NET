using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an pending file to be packed in ASAR archive
    /// </summary>
    public class AsarArchivePendingFile
    {
        /// <summary>
        /// Gets or sets <see langword="true"/> if packer must generate integrity data for this file
        /// </summary>
        public bool CalculateIntegrity { get; set; }
        /// <summary>
        /// Gets or sets <see langword="true"/> if packer should pack this file into ASAR archive file, instead of leave it in unpacked folder
        /// </summary>
        public bool IsUnpacked { get; set; }

        /// <summary>
        /// Gets full path to file which needs to be archived
        /// </summary>
        public string FilePath => FileInfo.FullName;

        /// <summary>
        /// Gets or sets file which needs to be archived
        /// </summary>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="AsarArchivePendingFile"/>
        /// </summary>
        /// <param name="fileInfo">File information about packing file</param>
        public AsarArchivePendingFile(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                throw new FileNotFoundException("File not found at specified location", fileInfo.Name);
            FileInfo= fileInfo;
            IsUnpacked = false;
        }
    }
}
