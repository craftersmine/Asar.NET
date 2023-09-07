using Newtonsoft.Json;
using System.Collections.Generic;

namespace craftersmine.Asar.Net
{
    internal class AsarArchiveFileSerialize
    {
        /// <summary>
        /// Gets size of file
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; set; }
        /// <summary>
        /// Gets offset of file in ASAR archive after header
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public string Offset { get; set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is executable
        /// </summary>
        [JsonProperty("executable", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsExecutable { get; set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is unpacked and located in *.asar.unpacked directory, otherwise <see langword="false"/>
        /// </summary>
        [JsonProperty("unpacked", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsUnpacked { get; set; }
        /// <summary>
        /// Gets <see langword="true"/> if file is a link
        /// </summary>
        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsLink { get; set; }

        [JsonProperty("integrity", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AsarArchiveFileIntegrity Integrity { get; set; } 
        
        [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, AsarArchiveFileSerialize> Files { get; set; }
    }
}
