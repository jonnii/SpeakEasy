using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    public class ByteArrayContent : IContent
    {
        public ByteArrayContent(string contentType, byte[] bytes)
        {
            ContentType = contentType;
            Content = bytes;
        }

        public int ContentLength => Content.Length;

        public byte[] Content { get; }

        public bool HasContent => ContentLength > 0;

        public string ContentType { get; }

        public Task WriteToAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            return stream.WriteAsync(Content, 0, Content.Length, cancellationToken);
        }

        public async Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (ContentLength <= 0)
            {
                return;
            }

            var memoryStream = new MemoryStream();
            await WriteToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            memoryStream.Position = 0;

            httpRequest.Content = new StreamContent(memoryStream);
            httpRequest.Content.Headers.ContentLength = memoryStream.Length;
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
        }
    }
}
