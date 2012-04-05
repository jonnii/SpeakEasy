using System;
using System.Net;

namespace Resticle
{
    public class HttpWebResponseWrapper : IHttpWebResponse
    {
        private readonly HttpWebResponse response;

        public HttpWebResponseWrapper(HttpWebResponse response)
        {
            this.response = response;
        }

        public Uri ResponseUri
        {
            get { return response.ResponseUri; }
        }

        public HttpStatusCode StatusCode
        {
            get { return response.StatusCode; }
        }
    }
}