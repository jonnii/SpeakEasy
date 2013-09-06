using System;
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

        public IHttpResponse Response
        {
            get { return response; }
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

        public T As<T>(Func<IHttpResponseHandler, T> constructor)
        {
            return constructor(this);
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
            var disposition = response.GetHeader("Content-Disposition");

            var parsedHeader = disposition.ParseValue();

            var name = parsedHeader.GetParameter("attachment", "name");
            var fileName = parsedHeader.GetParameter("attachment", "filename");

            return new FileDownload(
                name,
                fileName,
                response.ContentType,
                response.Body);
        }
    }
}