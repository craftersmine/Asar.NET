using System;
using System.Collections.Generic;
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
                .CreateBuilder(AsarArchiveOutputDir, AsarArchiveName).AddDirectory(TestPackDir1, true, true)
                .AddDirectory(TestPackDir2, true, true).AddFiles(true, true, TestEmptyFile, TestFile0)
                .CreateArchiveData();

            Assert.IsNotNull(AsarArchivePackerData, "Archive packer data is null");
        }
    }
}
