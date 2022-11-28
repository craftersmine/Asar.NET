using craftersmine.Asar.Net;

namespace craftersmine.Asar.NET.Tests
{
    [TestClass]
    public class AsarArchiveFileIntegrityTests
    {
        public const string DataHash = "40AFF2E9D2D8922E47AFD4648E6967497158785FBD1DA870E7110266BF944880";
        public const string BlockHash = "40AFF2E9D2D8922E47AFD4648E6967497158785FBD1DA870E7110266BF944880";

        [TestMethod]
        public async Task GetFileIntegrityTests()
        {
            MemoryStream stream = new MemoryStream(Data.RawData);
            Assert.IsNotNull(stream);
            Assert.AreNotEqual(0, stream.Length);

            AsarArchiveFileIntegrity fileIntegrity = await AsarArchiveFileIntegrity.GetStreamIntegrityAsync(stream);
            Assert.IsNotNull(fileIntegrity);
            Assert.AreEqual(DataHash, fileIntegrity.Hash);
            Assert.AreEqual(BlockHash, fileIntegrity.Blocks[0]);
            Assert.AreEqual("SHA256", fileIntegrity.Algorithm);
            Assert.AreEqual(4 * 1024 * 1024, fileIntegrity.BlockSize);

            bool isValid = await AsarArchiveFileIntegrity.ValidateStreamAsync(stream, fileIntegrity);
            Assert.IsTrue(isValid);
        }
    }
}