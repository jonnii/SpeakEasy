using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    public class StreamableContent : IContent
    {
        private readonly Func<Stream, CancellationToken, Task> onStream;

        public StreamableContent(string contentType, Func<Stream, CancellationToken, Task> onStream)
        {
            this.onStream = onStream;
            ContentType = contentType;
        }

        public int ContentLength { get; } = -1;

        public bool HasContent { get; } = true;

        public string ContentType { get; }

        public Task WriteToAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            return onStream(stream, cancellationToken);
        }
    }
}
