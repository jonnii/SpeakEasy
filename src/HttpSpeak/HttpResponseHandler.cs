namespace HttpSpeak
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        private readonly IHttpResponse response;

        public HttpResponseHandler(IHttpResponse response)
        {
            this.response = response;
        }

        public T Unwrap<T>()
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize<T>(response.Body);
        }

        public T Unwrap<T>(DeserializationSettings deserializationSettings)
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize<T>(response.Body, deserializationSettings);
        }
    }
}