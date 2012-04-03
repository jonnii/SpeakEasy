using System;

namespace Resticle
{
    public class RestClient : IRestClient
    {
        public RestClient(string root)
        {
            Root = root;
        }

        public string Root { get; set; }

        public Type DefaultSerializer { get; set; }

        public IRestResponse Get(string url, object segments = null)
        {
            return new RestResponse();
        }

        public IRestResponse Post(object body, string url, object segments = null)
        {
            throw new NotImplementedException();
        }

        public IRestResponse Put(object body, string url, object segments = null)
        {
            throw new NotImplementedException();
        }
    }
}