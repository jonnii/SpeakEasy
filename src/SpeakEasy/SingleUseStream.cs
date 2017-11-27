using System;
using System.IO;

namespace SpeakEasy
{
    public class SingleUseStream : IDisposable
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
                // TODO: Write a better exception message here
                throw new InvalidOperationException("You tried to use a stream twice");
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
