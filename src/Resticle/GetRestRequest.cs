using System;

namespace Resticle
{
    public class GetRestRequest : RestRequest
    {
        public GetRestRequest(Uri url)
            : base(url)
        {
        }
    }
}