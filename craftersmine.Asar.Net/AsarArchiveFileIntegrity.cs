using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        [JsonProperty("blockSize")]
        public int BlockSize { get; private set; }
        /// <summary>
        /// Gets file hashing algorithm
        /// </summary>
        [JsonProperty("algorithm")]
        public string Algorithm { get; private set; }
        /// <summary>
        /// Gets whole file hash
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; private set; }
        /// <summary>
        /// Gets hashes for file blocks of specified size
        /// </summary>
        [JsonProperty("blocks")]
        public string[] Blocks { get; private set; }

        private AsarArchiveFileIntegrity() {}

        /// <summary>
        /// Creates a new instance of ASAR archive file integrity data
        /// </summary>
        /// <param name="blockSize">Size of one block for hashing</param>
        /// <param name="algorithm">Algorithm used for hashing</param>
        /// <param name="hash">Whole file hash</param>
        /// <param name="blocks">Array of file blocks hashes</param>
        public AsarArchiveFileIntegrity(int blockSize, string algorithm, string hash, string[] blocks)
        {
            BlockSize = blockSize;
            Algorithm = algorithm;
            Hash = hash;
            Blocks = blocks;
        }

        /// <inheritdoc cref="AsarArchiveFileIntegrity"/>
        public AsarArchiveFileIntegrity(string hash, string[] blocks) : this(InternalBlockSize, InternalAlgorithmName, hash, blocks) {}

        /// <summary>
        /// Computes file hashes for specified file and returns <see cref="AsarArchiveFileIntegrity"/> with hashes
        /// </summary>
        /// <param name="filePath">Path to file for computing hashes</param>
        /// <returns>File integrity information</returns>
        public static async Task<AsarArchiveFileIntegrity> GetFileIntegrityAsync(string filePath)
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

            if (stream.Length > InternalBlockSize)
            {
                while (stream.Position != stream.Length)
                {
                    byte[] buffer = new byte[InternalBlockSize];
                    isEndOfStream = await stream.ReadAsync(buffer, currentOffset, buffer.Length) == 0;
                    string block = ByteArrayToString(_sha256CryptoServiceProvider.ComputeHash(buffer));
                    blocks.Add(block);
                    currentOffset += InternalBlockSize;
                }
            }

            string hash = ByteArrayToString(_sha256CryptoServiceProvider.ComputeHash(stream));

            blocks.Add(hash);

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

        /// <inheritdoc cref="object.Equals(object)"/>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (!(obj is AsarArchiveFileIntegrity))
                return false;

            AsarArchiveFileIntegrity other = (AsarArchiveFileIntegrity)obj;

            if (other.Blocks is null)
                return false;

            return this.Algorithm == other.Algorithm && this.BlockSize == other.BlockSize && this.Hash == other.Hash && this.Blocks.SequenceEqual(other.Blocks);
        }

        /// <inheritdoc cref="object.GetHashCode()"/>
        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Algorithm);
            sb.Append(BlockSize);
            sb.Append(Hash);
            sb.Append(Blocks.GetHashCode());
            return sb.ToString().GetHashCode();
        }

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            return Hash;
        }

        private static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }
    }
}
