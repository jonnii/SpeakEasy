using NLog;

namespace Resticle
{
    public class RequestRunner : IRequestRunner
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IWebRequestGateway webRequestGateway;

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IWebRequestGateway webRequestGateway)
        {
            this.transmissionSettings = transmissionSettings;
            this.webRequestGateway = webRequestGateway;
        }

        public IRestResponse Run(IRestRequest request)
        {
            Logger.Debug("running request of type {0}", request.GetType().Name);

            var webRequest = request.BuildWebRequest(transmissionSettings);

            return webRequestGateway.Send(webRequest, CreateRestResponse);
        }

        public IRestResponse CreateRestResponse(IHttpWebResponse webResponse)
        {
            var deserializer = transmissionSettings.FindSerializer(webResponse.ContentType);
            var body = webResponse.ReadBody();

            return new RestResponse(
                webResponse.ResponseUri,
                webResponse.StatusCode,
                body,
                deserializer);
        }
    }
}