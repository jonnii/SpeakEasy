using System;
using System.Net;

namespace Resticle
{
    public class PostRestRequest : RestRequest
    {
        public PostRestRequest(Uri url)
            : base(url)
        {
        }

        public override WebRequest BuildWebRequest()
        {
            var baseRequest = base.BuildWebRequest();

            baseRequest.Method = "POST";

            return baseRequest;
        }
    }
}