using System;
using System.Collections.Generic;
using System.Text;

namespace craftersmine.Asar.Net
{
    public class AsarArchiveFile
    {
        public long Offset { get; private set; }
        public long Size { get; private set; }
        public bool IsExecutable { get; private set; }
        public AsarArchiveFileIntegrity Integrity { get; private set; }
    }
}
