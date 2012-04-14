using System.IO;
using SpeakEasy.Extensions;

namespace SpeakEasy
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        private readonly IHttpResponse response;

        public HttpResponseHandler(IHttpResponse response)
        {
            this.response = response;
        }

        public T As<T>()
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize<T>(response.Body);
        }

        public T As<T>(DeserializationSettings deserializationSettings)
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize<T>(response.Body, deserializationSettings);
        }

        public byte[] AsByteArray()
        {
            return AsByteArray(16 * 1024);
        }

        public byte[] AsByteArray(int bufferSize)
        {
            var body = response.Body;

            var memoryStream = body as MemoryStream;
            return memoryStream != null
                ? memoryStream.ToArray()
                : body.ReadAsByteArray(bufferSize);
        }

        public string AsString()
        {
            using (var reader = new StreamReader(response.Body))
            {
                return reader.ReadToEnd();
            }
        }

        public IFile AsFile()
        {
            return new FileDownload("filename", "name", "contenttype", response.Body);
        }
    }
}