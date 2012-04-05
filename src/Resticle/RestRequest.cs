using System;

namespace Resticle
{
    public class GetRestRequest : IRestRequest
    {
        public GetRestRequest(Uri url)
        {
            Url = url;
        }

        public Uri Url { get; private set; }
    }
}