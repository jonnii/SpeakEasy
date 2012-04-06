namespace Resticle
{
    public interface IRequestRunner
    {
        IRestResponse Run(IRestRequest request);
    }

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
            var requestContext = new RequestContext(transmission, request);
            return requestContext.Send(webRequestGateway);
        }
    }
}