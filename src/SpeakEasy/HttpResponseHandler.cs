using System.IO;

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
            return null;
        }

        public string AsString()
        {
            using (var reader = new StreamReader(response.Body))
            {
                return reader.ReadToEnd();
            }
        }
    }
}