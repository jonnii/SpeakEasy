namespace Resticle
{
    public class RestClient : IRestClient
    {
        public RestClient(string root)
        {
            Root = root;
        }

        public string Root { get; set; }

        public IRestResponse Get(string url, object segments = null)
        {
            return new RestResponse();
        }
    }
}