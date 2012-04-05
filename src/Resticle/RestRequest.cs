using System;
using System.Net;

namespace Resticle
{
    public class GetRestRequest : IRestRequest
    {
        public GetRestRequest(Uri url)
        {
            Url = url;
        }

        public Uri Url { get; private set; }

        public WebRequest BuildWebRequest()
        {
            return WebRequest.Create(Url);
        }
    }
}