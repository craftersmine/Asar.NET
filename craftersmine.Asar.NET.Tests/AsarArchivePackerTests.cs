using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using craftersmine.Asar.Net;

namespace craftersmine.Asar.Net.Tests
{
    [TestClass]
    public class AsarArchivePackerTests
    {
        public const string AsarArchiveOutputDir = "asars\\test-pack";
        public const string AsarArchiveName = "test-asar";

        public const string TestPackDir1 = "TestsInput\\test-pack-data\\dir1";
        public const string TestPackDir2 = "TestsInput\\test-pack-data\\dir2";
        public const string TestEmptyFile = "TestsInput\\test-pack-data\\emptyfile.txt";
        public const string TestFile0 = "TestsInput\\test-pack-data\\file0.txt";

        public AsarArchivePackerData AsarArchivePackerData { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            AsarArchivePackerData = AsarArchivePackerDataBuilder
                .CreateBuilder(AsarArchiveOutputDir, AsarArchiveName).AddDirectory(TestPackDir1, false, true)
                .AddDirectory(TestPackDir2, true, true).AddFiles(false, true, TestEmptyFile, TestFile0)
                .CreateArchiveData();

            Assert.IsNotNull(AsarArchivePackerData, "Archive packer data is null");

            Debug.WriteLine("Cleaning up before packing...");
            if (Directory.Exists(AsarArchiveOutputDir))
                Directory.Delete(AsarArchiveOutputDir, true);
        }

        [TestMethod]
        public async Task PackArchiveTests()
        {
            AsarArchivePacker packer = new AsarArchivePacker(AsarArchivePackerData);
            Assert.IsNotNull(packer, "Packer is not created!");

            packer.StatusChanged += Packer_StatusChanged;
            packer.AsarArchivePacked += Packer_AsarArchivePacked;

            Assert.AreEqual(AsarArchivePackerData, packer.PackerData);

            await packer.PackAsync();
        }

        private void Packer_AsarArchivePacked(object? sender, AsarPackingCompletedEventArgs e)
        {
            Assert.IsNotNull(e.PackedArchive, "There is no opened ASAR archive after packing");
            Assert.IsFalse(string.IsNullOrWhiteSpace(e.AsarFilePath), "Packed ASAR file path is null or empty");
            Debug.WriteLine("ASAR output file: " + e.AsarFilePath);
        }

        private void Packer_StatusChanged(object? sender, AsarPackingEventArgs e)
        {
            Debug.WriteLine("Status: {2}. Packing {0} into {1}.", e.CurrentFileData?.GetPathInArchive(), e.FilePath, e.PackingStatus);
        }


    }
}
