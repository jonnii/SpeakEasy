using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    internal class StreamableContent : IContent
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

        public async Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            // this still needs stream management
            var memoryStream = new MemoryStream();
            await WriteToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            memoryStream.Position = 0;

            httpRequest.Content = new StreamContent(memoryStream);
            httpRequest.Content.Headers.ContentLength = memoryStream.Length;
            httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType);
        }
    }
}
