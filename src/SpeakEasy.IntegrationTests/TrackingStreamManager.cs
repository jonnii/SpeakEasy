using System;
using System.IO;
using System.Threading;

namespace SpeakEasy.IntegrationTests
{
    public class TrackingStreamManager : IStreamManager
    {
        private int numStreams;

        public Stream GetStream(string key)
        {
            Interlocked.Increment(ref numStreams);
            return new TrackingMemoryStream(this);
        }

        public void StreamDisposed()
        {
            Interlocked.Decrement(ref numStreams);
        }

        public void Reset()
        {
            numStreams = 0;
        }

        public void CheckForUnDisposedStreams()
        {
            if (numStreams != 0)
            {
                throw new Exception($"There are {numStreams} undisposed streams");
            }
        }

        public class TrackingMemoryStream : Stream
        {
            private readonly TrackingStreamManager manager;

            private readonly MemoryStream inner;

            public TrackingMemoryStream(TrackingStreamManager manager)
            {
                this.manager = manager;

                inner = new MemoryStream();
            }

            public override bool CanRead => inner.CanRead;

            public override bool CanSeek => inner.CanSeek;

            public override bool CanWrite => inner.CanWrite;

            public override long Length => inner.Length;

            public override long Position
            {
                get { return inner.Position; }
                set { inner.Position = value; }
            }

            public override void Flush()
            {
                inner.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return inner.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                inner.SetLength(value);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return inner.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                inner.Write(buffer, offset, count);
            }

            protected override void Dispose(bool disposing)
            {
                manager.StreamDisposed();
                base.Dispose(disposing);
            }
        }
    }
}
