using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace craftersmine.Asar.Net
{
    public static class AsarArchiveFileJsonSerialezer
    {
        public static string Serialeze(AsarArchiveFile asarArchiveFile)
        {
            var asarArchiveFileSerialize = ConvertToAsarArchiveFileSerialize(asarArchiveFile);
            string json = JsonConvert.SerializeObject(asarArchiveFileSerialize);
            return json;
        }

        public static AsarArchiveFile Deserialize(string json)
        {
            var asarArchiveFileSerialize = JsonConvert.DeserializeObject<AsarArchiveFileSerialize>(json);
            var asarArchiveFile = ConvertToAsarArchiveFile(asarArchiveFileSerialize, string.Empty);
            return asarArchiveFile;
        }

        private static AsarArchiveFile ConvertToAsarArchiveFile(AsarArchiveFileSerialize asarArchiveFileSerialize, string name)
        {
            long.TryParse(asarArchiveFileSerialize.Offset, out long offset);

            var asarArchiveFile = new AsarArchiveFile(offset, asarArchiveFileSerialize.Size, asarArchiveFileSerialize.IsUnpacked, asarArchiveFileSerialize.IsExecutable, name, asarArchiveFileSerialize.Integrity);
          
            if (asarArchiveFileSerialize.Files != null && asarArchiveFileSerialize.Files.Count() > 0)
            {
                foreach (var file in asarArchiveFileSerialize.Files)
                {
                    var asarArchiveFileChid = ConvertToAsarArchiveFile(file.Value, file.Key);
                    asarArchiveFile.Files.Add(file.Key, asarArchiveFileChid);
                }
            }

            return asarArchiveFile;
        }

        private static AsarArchiveFileSerialize ConvertToAsarArchiveFileSerialize(AsarArchiveFile asarArchiveFile)
        {
            var serialize = new AsarArchiveFileSerialize()
            {
                Size = asarArchiveFile.Size,
                Offset = asarArchiveFile.Offset.ToString(),
                IsLink = asarArchiveFile.IsLink,
                IsUnpacked = asarArchiveFile.IsUnpacked,
                Integrity = asarArchiveFile.Integrity,
                IsExecutable = asarArchiveFile.IsExecutable
            };

            if (asarArchiveFile.Files != null && asarArchiveFile.Files.Count() > 0)
            {
                var files = new Dictionary<string, AsarArchiveFileSerialize>();

                foreach (var file in asarArchiveFile.Files)
                {
                    var fileSerialize = ConvertToAsarArchiveFileSerialize(file.Value);
                    files.Add(file.Key, fileSerialize);
                }

                serialize.Files = files;
            }

            return serialize;
        }
    }
}
