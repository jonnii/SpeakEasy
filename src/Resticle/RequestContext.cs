using Resticle.Deserializers;

namespace Resticle
{
    public class RequestContext
    {
        private readonly ITransmission transmission;

        private readonly IRestRequest request;

        public RequestContext(ITransmission transmission, IRestRequest request)
        {
            this.transmission = transmission;
            this.request = request;
        }

        public IRestResponse Send(IWebRequestGateway webRequestGateway)
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