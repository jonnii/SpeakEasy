using System;
using System.IO;
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

        public string ReadBody()
        {
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null)
                {
                    throw new NotSupportedException("The body of the response stream could not be read.");
                }

                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}