using System;
using System.IO;
using System.Threading.Tasks;
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

        public object As(Type type)
        {
            var deserializer = response.Deserializer;

            return response.ConsumeBody(body => deserializer.Deserialize(body, type));
        }

        public object As(Type type, DeserializationSettings deserializationSettings)
        {
            var deserializer = response.Deserializer;

            return response.ConsumeBody(body => deserializer.Deserialize(body, deserializationSettings, type));
        }

        public T As<T>()
        {
            var deserializer = response.Deserializer;

            return response.ConsumeBody(body => deserializer.Deserialize<T>(body));
        }

        public T As<T>(DeserializationSettings deserializationSettings)
        {
            var deserializer = response.Deserializer;

            return response.ConsumeBody(body => deserializer.Deserialize<T>(body, deserializationSettings));
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
            return response.ConsumeBody(body =>
            {
                var memoryStream = body as MemoryStream;

                return memoryStream?.ToArray() ?? body.ReadAsByteArray(bufferSize);
            });
        }

        public string AsString()
        {
            return response.ConsumeBody(body =>
            {
                using (var reader = new StreamReader(body))
                {
                    return reader.ReadToEnd();
                }
            });
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
                f => response.ConsumeBody(f));
        }
    }
}
