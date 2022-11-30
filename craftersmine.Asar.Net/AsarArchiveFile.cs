using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an ASAR archive file
    /// </summary>
    public class AsarArchiveFile
    {
        [JsonIgnore]
        internal string FilePath { get; set; }

        /// <summary>
        /// Gets offset of file in ASAR archive after header
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public long Offset { get; private set; }
        /// <summary>
        /// Gets size of file
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; private set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is unpacked and located in *.asar.unpacked directory, otherwise <see langword="false"/>
        /// </summary>
        [JsonProperty("unpacked", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsUnpacked { get; private set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is executable
        /// </summary>
        [JsonProperty("executable", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsExecutable { get; private set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is a link
        /// </summary>
        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsLink { get; private set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is an ASAR archive root
        /// </summary>
        [JsonIgnore]
        public bool IsRoot { get; internal set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is an actual file and not a directory
        /// </summary>
        [JsonIgnore]
        internal bool IsFile { get; set; }
        /// <summary>
        /// Gets name of file in ASAR archive
        /// </summary>
        [JsonIgnore]
        public string Name { get; internal set; }
        /// <summary>
        /// Gets file integrity information
        /// </summary>
        [JsonProperty("integrity", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AsarArchiveFileIntegrity Integrity { get; private set; }
        /// <summary>
        /// Gets child files if it is a directory
        /// </summary>
        [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AsarArchiveFileCollection Files { get; internal set; }
        /// <summary>
        /// Gets file parent file (directory)
        /// </summary>
        [JsonIgnore]
        public AsarArchiveFile Parent { get; internal set; }

        internal AsarArchiveFile(long offset, long size, bool isUnpacked, bool isExecutable, string name, AsarArchiveFileIntegrity integrity)
        {
            Offset = offset;
            Size = size;
            IsUnpacked = isUnpacked;
            IsExecutable = isExecutable;
            Name = name;
            Integrity = integrity;

            Files = new AsarArchiveFileCollection();
        }

        internal AsarArchiveFile()
        {
            Files = new AsarArchiveFileCollection();
        }

        /// <summary>
        /// Gets total count of child files in this file (directory)
        /// </summary>
        /// <returns></returns>
        public int GetFileCount()
        {
            int fileCount = 0;
            if (!(Files is null) && Files.Any())
            {
                foreach (AsarArchiveFile f in Files.Values)
                {
                    fileCount += f.GetFileCount();
                }
            }
            else
                fileCount++;
            return fileCount;
        }

        /// <summary>
        /// Gets file path within ASAR archive
        /// </summary>
        /// <returns></returns>
        public string GetPathInArchive()
        {
            string path = Name;
            if (Name is null)
                path = string.Empty;
            if (!(Parent is null))
                path = Path.Combine(Parent.GetPathInArchive(), path);
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeOffset() => IsFile && !IsUnpacked;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeSize() => IsFile;
    }
}
