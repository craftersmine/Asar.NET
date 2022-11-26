using craftersmine.Asar.Net;

namespace craftersmine.Asar.NET.Tests
{
    [TestClass]
    public class AsarArchiveFileIntegrityTests
    {
        public const string DataHash = "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855";
        public const string BlockHash = "88E0700AE7D36F20FF85008B0E644B79C1A58F5AE29594960725DCA2EEBD6AAE";

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