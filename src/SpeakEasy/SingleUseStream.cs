using System;
using System.IO;

namespace SpeakEasy
{
    internal class SingleUseStream : IDisposable
    {
        private readonly Stream stream;

        private bool isConsumed;

        public SingleUseStream(Stream stream)
        {
            this.stream = stream;
        }

        public Stream GetAndConsumeStream()
        {
            if (isConsumed)
            {
                throw new InvalidOperationException(
                    "An attempt was made to consume the same stream twice. This can happen if you " +
                    "try to do two things with an http response.");
            }

            isConsumed = true;

            return stream;
        }

        public void Dispose()
        {
            isConsumed = true;
            stream.Dispose();
        }
    }
}
