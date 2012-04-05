using System;

namespace Resticle
{
    public class RestRequest : IRestRequest
    {
        public RestRequest(Uri url)
        {
            Url = url;
        }

        public Uri Url { get; private set; }

        public object Body { get; set; }
    }
}