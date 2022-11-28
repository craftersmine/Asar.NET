using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace craftersmine.Asar.Net
{
    public class AsarArchiveFile
    {
        [JsonProperty("offset")]
        public long Offset { get; private set; }
        [JsonProperty("size")]
        public long Size { get; private set; }
        [JsonProperty("unpacked")]
        public bool IsUnpacked { get; private set; }
        [JsonProperty("executable")]
        public bool IsExecutable { get; private set; }
        public bool IsRoot { get; internal set; }
        public string Name { get; internal set; }
        [JsonProperty("integrity")]
        public AsarArchiveFileIntegrity Integrity { get; private set; }
        [JsonProperty("files")]
        public AsarArchiveFileCollection Files { get; private set; }

        public AsarArchiveFile Parent { get; internal set; }
    }
}
