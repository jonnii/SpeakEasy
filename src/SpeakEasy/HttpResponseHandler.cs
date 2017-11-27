using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class HttpResponseHandler : IHttpResponseHandler
    {
        private readonly IHttpResponse response;

        private readonly ISerializer serializer;

        private readonly SingleUseStream body;

        public HttpResponseHandler(IHttpResponse response, ISerializer serializer, SingleUseStream body)
        {
            this.response = response;
            this.serializer = serializer;
            this.body = body;
        }

        public IHttpResponse Response => response;

        public object As(Type type)
        {
            return serializer.Deserialize(body.GetAndConsumeStream(), type);
        }

        public T As<T>()
        {
            return serializer.Deserialize<T>(body.GetAndConsumeStream());
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
            using (var copy = new MemoryStream())
            {
                await body.GetAndConsumeStream().CopyToAsync(copy, bufferSize).ConfigureAwait(false);
                return copy.ToArray();
            }
        }

        public async Task<string> AsString()
        {
            using (var reader = new StreamReader(body.GetAndConsumeStream()))
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
                body.GetAndConsumeStream());
        }
    }
}
