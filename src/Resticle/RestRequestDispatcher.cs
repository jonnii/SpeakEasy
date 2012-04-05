namespace Resticle
{
    public class RestRequestDispatcher : IRestRequestDispatcher
    {
        private readonly IWebRequestGateway webRequestGateway;

        public RestRequestDispatcher()
            : this(new WebRequestGateway())
        {
        }

        public RestRequestDispatcher(IWebRequestGateway webRequestGateway)
        {
            this.webRequestGateway = webRequestGateway;
        }

        public IRestResponse Dispatch(IRestRequest request)
        {
            var webRequest = request.BuildWebRequest();

            return webRequestGateway.Send(webRequest, CreateRestResponse);
        }

        public RestResponse CreateRestResponse(IHttpWebResponse webResponse)
        {
            return new RestResponse(
                webResponse.ResponseUri,
                webResponse.StatusCode);
        }
    }
}