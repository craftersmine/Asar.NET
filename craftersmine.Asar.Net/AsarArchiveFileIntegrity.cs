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
    /// <summary>
    /// Represents a ASAR archive file integrity information. This class cannot be inherited
    /// </summary>
    public sealed class AsarArchiveFileIntegrity
    {
        private const string InternalAlgorithmName = "SHA256";
        private const int InternalBlockSize = 4 * 1024 * 1024;
        private static readonly SHA256CryptoServiceProvider _sha256CryptoServiceProvider = new SHA256CryptoServiceProvider();

        /// <summary>
        /// Gets file block size
        /// </summary>
        [JsonPropertyName("blockSize")]
        public int BlockSize { get; private set; }
        /// <summary>
        /// Gets file hashing algorithm
        /// </summary>
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; private set; }
        /// <summary>
        /// Gets whole file hash
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; private set; }
        /// <summary>
        /// Gets hashes for file blocks of specified size
        /// </summary>
        [JsonPropertyName("blocks")]
        public string[] Blocks { get; private set; }

        private AsarArchiveFileIntegrity() {}

        public static async Task<AsarArchiveFileIntegrity> GetFileIntegrity(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
                return await GetStreamIntegrityAsync(stream);
        }
        
        /// <summary>
        /// Computes hashes for specified stream and returns <see cref="AsarArchiveFileIntegrity"/> with hashes
        /// </summary>
        /// <param name="stream">Data stream for computing hashes</param>
        /// <returns>Stream integrity information</returns>
        public static async Task<AsarArchiveFileIntegrity> GetStreamIntegrityAsync(Stream stream)
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

        /// <summary>
        /// Validates file at specified path against specified integrity data and returns <see langword="true"/> if data is same, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="filePath">Path to file for checks</param>
        /// <param name="integrityData">Integrity data for checks</param>
        /// <returns><see langword="true"/> if file valid, otherwise <see langword="false"/></returns>
        public static async Task<bool> ValidateFileAsync(string filePath, AsarArchiveFileIntegrity integrityData)
        {
            using (FileStream fs = File.OpenRead(filePath))
                return await ValidateStreamAsync(fs, integrityData);
        }
        
        /// <summary>
        /// Validates data in stream against specified integrity data and returns <see langword="true"/> if data is same, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="stream">Data stream for checks</param>
        /// <param name="integrityData">Integrity data for checks</param>
        /// <returns><see langword="true"/> if file valid, otherwise <see langword="false"/></returns>
        public static async Task<bool> ValidateStreamAsync(Stream stream, AsarArchiveFileIntegrity integrityData)
        {
            stream.Seek(0, SeekOrigin.Begin);
            
            AsarArchiveFileIntegrity streamIntegrity = await GetStreamIntegrityAsync(stream);

            if (streamIntegrity.Algorithm != integrityData.Algorithm) return false;

            if (streamIntegrity.BlockSize != integrityData.BlockSize) return false;

            if (streamIntegrity.Hash != integrityData.Hash) return false;

            if (streamIntegrity.Blocks.Length != integrityData.Blocks.Length) return false;

            for (int i = 0; i < integrityData.Blocks.Length; i++)
                if (streamIntegrity.Blocks[i] != integrityData.Blocks[i]) return false;

            return true;
        }

        private static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }
    }
}
