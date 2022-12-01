using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using craftersmine.Asar.Net;

namespace craftersmine.Asar.Net.Tests
{
    [TestClass]
    public class AsarArchiveTests
    {
        public const string AsarArchivePath = "TestsInput\\packthis-unpack.asar";

        public AsarArchive Archive { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Archive = new AsarArchive(AsarArchivePath);
            Assert.IsNotNull(Archive, "Archive is null, not loaded or corrupted");
            Assert.IsNotNull(Archive.Files, "Archive does not contain root, corrupted or failed to read");
            Assert.IsTrue(Archive.Files.Files.Any(), "Archive root does not contain any files or directories");
            Assert.IsTrue(Archive.Files.IsRoot, "Archive root is not a root");
            Assert.IsTrue(Archive.IsFile, "Archive is loaded other than file");
            Assert.IsTrue(Archive.FilePath.Contains(AsarArchivePath), "Path to the archive does not contain " + AsarArchivePath);
            Assert.AreEqual(1292, Archive.FilesDataOffset, "Offset to the files data is not 1292");
            Assert.AreEqual(1276, Archive.HeaderSize, "Archive header metadata size is not 1276");
            Assert.IsTrue(Archive.UnpackedFilesPath.Contains("TestsInput\\packthis-unpack.asar.unpacked"), "Unpacked directory path is not correct");
            Assert.AreEqual(5, Archive.Files.GetFileCount(), "Total count of files is not 5");
        }

        [TestMethod]
        public void ReadFilesTests()
        {
            AsarArchiveFile dir2 = Archive.FindFile("dir2\\");
            Assert.IsNotNull(dir2, "dir2 is missing in the archive");
            Assert.IsNotNull(dir2.Files, "dir2 is missing child elements data");
            Assert.IsTrue(dir2.Files.Any(), "dir2 does not contain any files or directories");

            AsarArchiveFile file3 = Archive.FindFile("dir2\\file3.txt");
            Assert.IsNotNull(file3, "dir2\\file3.txt is missing in the archive");
            Assert.IsFalse(file3.Files.Any(), "dir2\\file3.txt contains child elements, but it is a text file");
            Assert.IsFalse(file3.IsUnpacked, "dir2\\file3.txt is unpacked, but it should be packed");
            Assert.AreEqual(3, file3.Size, "dir2\\file3.txt size is not 3 bytes");
            Assert.AreEqual(9, file3.Offset, "dir2\\file3.txt offset is not 9");
            AsarArchiveFileIntegrity file3Integrity = file3.Integrity;
            Assert.IsNotNull(file3Integrity, "dir2\\file3.txt integrity data is missing");
            Assert.AreEqual("SHA256", file3Integrity.Algorithm, "dir2\\file3.txt hashing algorithm is not SHA256");
            Assert.AreEqual("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", file3Integrity.Hash, "dir2\\file3.txt hashes mismatched in integrity data");
            Assert.AreEqual(4194304, file3Integrity.BlockSize, "dir2\\file3.txt integrity block data is not 4MB (4194304)");
            Assert.IsNotNull(file3Integrity.Blocks, "dir2\\file3.txt integrity data missing blocks data");
            Assert.IsTrue(file3Integrity.Blocks.Any(), "dir2\\file3.txt integrity data does not contain any block integrity data");
            Assert.AreEqual("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", file3Integrity.Blocks[0], "dir2\\file3.txt first block hash is mismatched in integrity data");

            AsarArchiveFile file2 = Archive.FindFile("dir2\\file2.png");
            Assert.IsNotNull(file2, "dir2\\file2.png is missing in the archive");
            Assert.IsFalse(file2.Files.Any(), "dir2\\file2.png contains child elements, but it is a PNG image");
            Assert.IsTrue(file2.IsUnpacked, "dir2\\file2.png is packed, but it should be unpacked");
            Assert.IsFalse(file2.IsExecutable, "dir2\\file2.png is executable, but it is a PNG image");
            Assert.IsFalse(file2.IsLink, "dir2\\file2.png is link, but it is not a link in the archive");
            Assert.AreEqual("file2.png", file2.Name, "dir2\\file2.png file name is not file2.png");
            Assert.AreEqual(dir2, file2.Parent, "dir2\\file2.png parent is not dir2");
            Assert.AreEqual(182, file2.Size, "dir2\\file2.png size is not 182 bytes");
            AsarArchiveFileIntegrity file2Integrity = file2.Integrity;
            Assert.IsNotNull(file2Integrity, "dir2\\file2.png integrity data is missing");
            Assert.AreEqual("SHA256", file2Integrity.Algorithm, "dir2\\file2.png hashing algorithm is not SHA256");
            Assert.AreEqual("cc402b796dc92b2b1f3a6d09515003d8400e63d8acaffc967e49c0cf015fcffe", file2Integrity.Hash, "dir2\\file2.png hashes mismatched in integrity data");
            Assert.AreEqual(4194304, file2Integrity.BlockSize, "dir2\\file2.png integrity block size is not 4MB, (4194304)");
            Assert.IsNotNull(file2Integrity.Blocks, "dir2\\file2.png integrity data missing blocks data");
            Assert.IsTrue(file2Integrity.Blocks.Any(), "dir2\\file2.png integrity data does not contain any block integrity data");
            Assert.AreEqual("cc402b796dc92b2b1f3a6d09515003d8400e63d8acaffc967e49c0cf015fcffe", file2Integrity.Blocks[0], "dir2\\file2.png first block hash is mismatched in integrity data");
        }
    }
}
