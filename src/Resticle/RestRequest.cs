using System;
using System.Net;

namespace Resticle
{
    public abstract class RestRequest : IRestRequest
    {
        protected RestRequest(string url)
        {
            Url = new Uri(url);
        }

        public Uri Url { get; private set; }

        public virtual WebRequest BuildWebRequest()
        {
            var request = WebRequest.Create(Url);

            request.ContentLength = 0;

            return request;
        }
    }
}