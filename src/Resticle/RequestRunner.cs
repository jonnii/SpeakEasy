namespace Resticle
{
    public class RequestRunner : IRequestRunner
    {
        private readonly ITransmission transmission;

        private readonly IWebRequestGateway webRequestGateway;

        public RequestRunner(ITransmission transmission, IWebRequestGateway webRequestGateway)
        {
            this.transmission = transmission;
            this.webRequestGateway = webRequestGateway;
        }

        public IRestResponse Run(IRestRequest request)
        {
            var webRequest = request.BuildWebRequest(transmission);

            return webRequestGateway.Send(webRequest, CreateRestResponse);
        }

        public IRestResponse CreateRestResponse(IHttpWebResponse webResponse)
        {
            var deserializer = transmission.FindDeserializer(webResponse.ContentType);
            var body = webResponse.ReadBody();

            return new RestResponse(
                webResponse.ResponseUri,
                webResponse.StatusCode,
                body,
                deserializer);
        }
    }
}