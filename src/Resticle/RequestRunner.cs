using Resticle.Deserializers;

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

        public RestResponse CreateRestResponse(IHttpWebResponse webResponse)
        {
            var deserializer = new JsonDeserializer();

            var body = webResponse.ReadBody();

            return new RestResponse(
                deserializer,
                webResponse.ResponseUri,
                webResponse.StatusCode,
                body);
        }

    }
}