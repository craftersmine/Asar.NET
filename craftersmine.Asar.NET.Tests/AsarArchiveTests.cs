using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using craftersmine.Asar.Net;

namespace craftersmine.Asar.Net.Tests
{
    [TestClass]
    public class AsarArchiveTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod, DeploymentItem("TestsInput\\packthis-unpack.asar"), DeploymentItem("craftersmine.Asar.Net.dll"), DeploymentItem("TestsInput")]
        public async Task LoadAsarTests()
        {
            //AsarArchive archive = new AsarArchive("TestsInput\\packthis-unpack.asar");

            //AsarArchiveFile file = archive.FindFile("dir2/file3.txt");
            //Stream stream = archive.OpenFileAsStream(file);
            ////byte[] data = await archive.ReadBytes(file);
            //byte[] data = new byte[stream.Length];
            //int read = stream.Read(data, 0, data.Length);
            //string dataString = Encoding.Default.GetString(data);

            //byte[] dataFile0 = await archive.ReadBytesAsync("file0.txt");
            //string dataStringFile0 = Encoding.Default.GetString(dataFile0);

            //AsarArchivePackerDataBuilder builder = AsarArchivePackerDataBuilder.CreateBuilder("asars\\firstAsar", "testAsarArc");
            //AsarArchivePackerData data = builder
            //    .AddDirectory("TestsInput//packthis-unpack.asar.unpacked", true)
            //    .AddFile("craftersmine.Asar.Net.dll",
            //        false,
            //        true)
            //    .PerformFileSort(true)
            //    .CreateArchiveData();
            //AsarArchivePacker packer = new AsarArchivePacker(data);

            //packer.OnAsarArchivePacked += PackerOnOnAsarArchivePacked;
            //await packer.PackAsync();

            AsarArchive archive = new AsarArchive("TestsInput\\packthis-unpack.asar");
            AsarArchiveUnpacker unpacker = new AsarArchiveUnpacker(archive);
            await unpacker.UnpackAsync("asars\\unpacked-packthis-unpack");
        }

        private void PackerOnOnAsarArchivePacked(object? sender, AsarPackingCompletedEventArgs e)
        {
            int files = e.PackedArchive.Files.GetFileCount();
            Assert.IsNotNull(e.PackedArchive);
        }
    }
}
