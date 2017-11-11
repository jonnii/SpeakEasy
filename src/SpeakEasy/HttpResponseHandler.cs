using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        private readonly IHttpResponse response;

        public HttpResponseHandler(IHttpResponse response)
        {
            this.response = response;
        }

        public IHttpResponse Response => response;

        public object As(Type type)
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize(response.Body, type);
        }

        public T As<T>()
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize<T>(response.Body);
        }

        public T As<T>(Func<IHttpResponseHandler, T> constructor)
        {
            return constructor(this);
        }

        public Task<byte[]> AsByteArray()
        {
            return AsByteArray(16 * 1024);
        }

        public async Task<byte[]> AsByteArray(int bufferSize)
        {
            var body = response.Body;

            if (body is MemoryStream memoryStream)
            {
                return memoryStream.ToArray();
            }

            using (var copy = new MemoryStream())
            {
                await body.CopyToAsync(copy, bufferSize).ConfigureAwait(false);
                return copy.ToArray();
            }
        }

        public async Task<string> AsString()
        {
            using (var reader = new StreamReader(response.Body))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public IFile AsFile()
        {
            var contentDisposition = response.Headers.ContentDisposition;

            return new FileDownload(
                contentDisposition.Name,
                contentDisposition.FileName,
                response.ContentType,
                response.Body);
        }
    }
}
