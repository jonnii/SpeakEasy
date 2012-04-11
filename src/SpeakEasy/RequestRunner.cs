using NLog;

namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IWebRequestGateway webRequestGateway;

        private readonly IAuthenticator authenticator;

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IWebRequestGateway webRequestGateway,
            IAuthenticator authenticator)
        {
            this.transmissionSettings = transmissionSettings;
            this.webRequestGateway = webRequestGateway;
            this.authenticator = authenticator;
        }

        public IHttpResponse Run(IHttpRequest request)
        {
            Logger.Debug("running request of type {0}", request.GetType().Name);

            authenticator.Authenticate(request);

            var webRequest = request.BuildWebRequest(transmissionSettings);
            return webRequestGateway.Send(webRequest, CreateHttpResponse);
        }

        public IHttpResponse CreateHttpResponse(IHttpWebResponse webResponse)
        {
            var deserializer = transmissionSettings.FindSerializer(webResponse.ContentType);
            var body = webResponse.ReadBody();

            return new HttpResponse(
                webResponse.ResponseUri,
                webResponse.StatusCode,
                body,
                deserializer);
        }
    }
}