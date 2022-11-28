using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an Electron ASAR archive
    /// </summary>
    public partial class AsarArchive : IDisposable
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
