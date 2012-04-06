namespace Resticle
{
    public class RestResponseHandler : IRestResponseHandler
    {
        private readonly IRestResponse response;
        private readonly IDeserializer deserializer;

        public RestResponseHandler(
            IRestResponse response,
            IDeserializer deserializer)
        {
            this.response = response;
            this.deserializer = deserializer;
        }

        public T Unwrap<T>()
        {
            return deserializer.Deserialize<T>(response.Body);
        }
    }
}