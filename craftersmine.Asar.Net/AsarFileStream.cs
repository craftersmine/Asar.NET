using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents a stream of data from file within ASAR archive
    /// </summary>
    public class AsarFileStream : MemoryStream
    {
        private int filesOffset = 0;
        private int fileBeginingOffset = 0;

        private AsarFileStream() {}

        internal AsarFileStream(Stream archiveStream, int filesOffset, int fileBeginingOffset, int fileLength)
        {
            if (archiveStream is null)
                throw new ArgumentNullException(nameof(archiveStream));
            if (filesOffset <= 0)
                throw new ArgumentOutOfRangeException(nameof(filesOffset),
                    "Files data offset cannot be less or equals to 0");
            if (fileBeginingOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(fileBeginingOffset),
                    "File data offset cannot be less or equals to 0");
            if (fileLength < 0)
                throw new ArgumentOutOfRangeException(nameof(fileLength),
                    "File data offset cannot be less or equals to 0");

            if (!archiveStream.CanRead)
                throw new AsarException("Cannot read from ASAR archive stream");

            this.filesOffset = filesOffset;
            this.fileBeginingOffset = fileBeginingOffset;

            archiveStream.Seek(filesOffset + fileBeginingOffset, SeekOrigin.Begin);

            int bytesRead = 0;
            do
            {
                int buffSize = 4096;
                if (fileLength - Length < buffSize)
                    buffSize = (int)(fileLength - bytesRead);

                byte[] buffer = new byte[buffSize];
                bytesRead += archiveStream.Read(buffer, 0, buffSize);
                this.Write(buffer, 0, buffer.Length);
            } while (Length < fileLength);

            this.Position = 0;
        }
    }
}
