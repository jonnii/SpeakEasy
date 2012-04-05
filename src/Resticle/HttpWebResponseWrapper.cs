using System;
using System.Net;

namespace Resticle
{
    public class HttpWebResponseWrapper : IHttpWebResponse
    {
        private readonly WebResponse response;

        public HttpWebResponseWrapper(WebResponse response)
        {
            this.response = response;
        }

        public Uri ResponseUri
        {
            get { return response.ResponseUri; }
        }
    }
}