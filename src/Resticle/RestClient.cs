using System;

namespace Resticle
{
    public class RestClient : IRestClient
    {
        public RestClient()
        {

        }

        public RestClient(string root)
        {
            Root = new Resource(root);
        }

        public Resource Root { get; set; }

        public Type DefaultSerializer { get; set; }

        public IRestResponse Get(string url, object segments = null)
        {
            var resource = Root.Append(url);
            var mergedResource = resource.Merge(segments);

            return new RestResponse(new Uri(mergedResource));
        }

        public IRestResponse Post(object body, string url, object segments = null)
        {
            throw new NotImplementedException();
        }

        public IRestResponse Put(object body, string url, object segments = null)
        {
            throw new NotImplementedException();
        }

        public IRestResponse Delete(string url, object segments = null)
        {
            throw new NotImplementedException();
        }

        public IRestRequestBuilder NewRequest(string url, object segments = null)
        {
            return new RestRequestBuilder(Root, url, segments);
        }
    }
}