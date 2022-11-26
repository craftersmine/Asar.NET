using System;
using System.IO;

namespace craftersmine.Asar.Net
{
    public class AsarArchive
    {
        private Stream archiveStream;

        public long HeaderSize { get; private set; }
        public AsarArchiveFileCollection Files { get; private set; }

        public AsarArchive(Stream stream)
        {
            archiveStream = stream;
        }

        public AsarArchive(string fileName)
        {
            archiveStream = File.OpenRead(fileName);
        }
    }
}
