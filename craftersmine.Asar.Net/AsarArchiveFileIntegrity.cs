using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace craftersmine.Asar.Net
{
    public class AsarArchiveFileIntegrity
    {
        private const string InternalAlgorithmName = "SHA256";
        private const int InternalBlockSize = 4 * 1024 * 1024;
        private static readonly SHA256CryptoServiceProvider _sha256CryptoServiceProvider = new SHA256CryptoServiceProvider();

        [JsonPropertyName("blockSize")]
        public int BlockSize { get; private set; }
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; private set; }
        [JsonPropertyName("hash")]
        public string Hash { get; private set; }
        [JsonPropertyName("blocks")]
        public string[] Blocks { get; private set; }

        private AsarArchiveFileIntegrity() {}

        public static async Task<AsarArchiveFileIntegrity> GetFileIntegrity(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
                return await GetFileIntegrity(stream);
        }

        public static async Task<AsarArchiveFileIntegrity> GetFileIntegrity(Stream stream)
        {
            _sha256CryptoServiceProvider.Initialize();

            bool isEndOfStream = false;
            List<string> blocks = new List<string>();
            int currentOffset = 0;

            while (stream.Position != stream.Length)
            {
                byte[] buffer = new byte[InternalBlockSize];
                isEndOfStream = await stream.ReadAsync(buffer, currentOffset, buffer.Length) == 0;
                string block = ByteArrayToString(_sha256CryptoServiceProvider.ComputeHash(buffer));
                blocks.Add(block);
                currentOffset += InternalBlockSize;
            }

            string hash = ByteArrayToString(_sha256CryptoServiceProvider.ComputeHash(stream));

            return new AsarArchiveFileIntegrity()
                {BlockSize = InternalBlockSize, Algorithm = InternalAlgorithmName, Blocks = blocks.ToArray(), Hash = hash};
        }

        private static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }
    }
}
