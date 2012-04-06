using System;
using System.Net;

namespace Resticle
{
    public abstract class RestRequest : IRestRequest
    {
        protected RestRequest(Uri url)
        {
            Url = url;
        }

        public Uri Url { get; private set; }

        public virtual WebRequest BuildWebRequest()
        {
            return WebRequest.Create(Url);
        }
    }
}