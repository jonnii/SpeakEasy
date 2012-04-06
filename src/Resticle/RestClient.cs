using System;
using Newtonsoft.Json;

namespace Resticle
{
    public class RestClient : IRestClient
    {
        public RestClient()
        {
            Dispatcher = new RestRequestDispatcher();
        }

        public RestClient(string root)
            : this()
        {
            Root = new Resource(root);
        }

        public Resource Root { get; set; }

        public Type DefaultSerializer { get; set; }

        public IRestRequestDispatcher Dispatcher { get; set; }

        public IRestResponse Get(string relativeUrl, object segments = null)
        {
            var url = Root.Append(relativeUrl).Merge(segments);
            var request = new GetRestRequest(url);

            return Dispatcher.Dispatch(request);
        }

        public IRestResponse Post(object body, string relativeUrl, object segments = null)
        {
            var url = Root.Append(relativeUrl).Merge(segments);

            var request = new PostRestRequest(url)
            {
                Body = () => JsonConvert.SerializeObject(body)
            };

            return Dispatcher.Dispatch(request);
        }

        public IRestResponse Put(object body, string relativeUrl, object segments = null)
        {
            throw new NotImplementedException();
        }

        public IRestResponse Delete(string relativeUrl, object segments = null)
        {
            throw new NotImplementedException();
        }
    }
}