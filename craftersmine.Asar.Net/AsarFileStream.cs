using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents a stream of data from file within ASAR archive
    /// </summary>
    public class AsarFileStream : Stream
    {
        private readonly Stream archiveStream;
        private int filesOffset = 0;
        private int fileBeginingOffset = 0;

        /// <summary>
        /// Gets <see langword="true"/> if you can read from this stream
        /// </summary>
        public override bool CanRead => archiveStream.CanRead;
        /// <summary>
        /// Gets <see langword="true"/> if you can seek this stream
        /// </summary>
        public override bool CanSeek => archiveStream.CanSeek;
        /// <summary>
        /// Gets <see langword="true"/> if you can write to this stream
        /// </summary>
        public override bool CanWrite => false;
        /// <summary>
        /// Gets total length of stream
        /// </summary>
        public override long Length { get; }
        /// <summary>
        /// Gets current position within stream
        /// </summary>
        public override long Position { get; set; }

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

            this.archiveStream = archiveStream;
            this.filesOffset = filesOffset;
            this.fileBeginingOffset = fileBeginingOffset;
            Length = fileLength;
        }

        /// <inheritdoc cref="Stream.Flush"/>
        public override void Flush()
        {
            archiveStream.Flush();
        }

        /// <inheritdoc cref="Stream.Read"/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            archiveStream.Seek(filesOffset + fileBeginingOffset + Position, SeekOrigin.Begin);
            int read = archiveStream.Read(buffer, offset, count);
            Position += read;
            return read;
        }
        
        /// <inheritdoc cref="Stream.Seek"/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = 0;
                    long newPosBeg = Position + offset;
                    Position = newPosBeg;
                    if (newPosBeg > Length)
                        Position = Length;
                    break;
                case SeekOrigin.Current:
                    long newPosCur = Position + offset;
                    Position = newPosCur;
                    if (newPosCur > Length)
                        Position = Length;
                    break;
                case SeekOrigin.End:
                    Position = Length;
                    long newPosEnd = Position - offset;
                    Position = newPosEnd;
                    if (newPosEnd < 0)
                        Position = 0;
                    break;
            }

            return Position;
        }
        
        /// <inheritdoc cref="Stream.SetLength"/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("Setting length for ASAR file stream is not supported");
        }
        
        /// <inheritdoc cref="Stream.Write"/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Writing to an already packed ASAR archive is not supported");
        }
    }
}
