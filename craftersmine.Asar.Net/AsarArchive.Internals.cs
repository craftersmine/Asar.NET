using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Asar.Net
{
    public partial class AsarArchive
    {
        private void PopulateParents(AsarArchiveFile file)
        {
            foreach (KeyValuePair<string, AsarArchiveFile> f in file.Files)
            {
                f.Value.Parent = file;
                f.Value.Name = f.Key;
                if (!(f.Value.Files is null) && f.Value.Files.Any())
                    PopulateParents(f.Value);
            }
        }
        
        private string GetPathForFile(AsarArchiveFile file)
        {
            if (file.IsRoot)
                return string.Empty;
            string path = file.Name;
            if (!(file.Parent is null))
                path = Path.Combine(GetPathForFile(file.Parent), path);
            return path;
        }

        private async Task<byte[]> ReadUnpackedBytesAsync(AsarArchiveFile file)
        {
            using (FileStream fs = OpenUnpackedAsStream(file))
            {
                byte[] buffer = new byte[file.Size];
                int read = await fs.ReadAsync(buffer, 0, (int)file.Size);
                return buffer;
            }
        }

        private FileStream OpenUnpackedAsStream(AsarArchiveFile file)
        {
            if (!IsFile)
                throw new AsarException(
                    "Unable to read unpacked file for ASAR archive from Stream! This only available for archive that are read from file");

            string unpackedPath = Path.Combine(UnpackedFilesPath, GetPathForFile(file));

            FileStream unpackedStream = File.OpenRead(unpackedPath);
            return unpackedStream;
        }
    }
}
