using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Asar.Net
{
    public class AsarArchiveUnpacker
    {
        private string outputDir = string.Empty;

        public AsarArchive Archive { get; private set; }

        // TODO: implement events

        public AsarArchiveUnpacker(AsarArchive archive)
        {
            Archive = archive;
        }

        public async Task UnpackAsync(string outputDir)
        {
            this.outputDir = outputDir;

            await UnpackFileAsync(Archive.Files);
        }

        private async Task UnpackFileAsync(AsarArchiveFile file)
        {
            string filePath = Path.Combine(outputDir, file.GetPathInArchive());
            if (!(file.Files is null) && file.Files.Any())
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                foreach (AsarArchiveFile f in file.Files.Values)
                {
                    await UnpackFileAsync(f);
                }
            }
            else
            {
                if (!file.IsUnpacked)
                {
                    using (AsarFileStream asarFileStream = Archive.OpenFileAsStream(file) as AsarFileStream)
                    {
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            await asarFileStream?.CopyToAsync(fileStream);
                        }
                    }
                }
                else
                {
                    using (Stream asarFileStream = Archive.OpenFileAsStream(file))
                    {
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            await asarFileStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }
    }
}
