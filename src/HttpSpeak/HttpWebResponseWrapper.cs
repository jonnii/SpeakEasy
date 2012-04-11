using System;
using System.IO;
using System.Net;
using System.Net.Mime;

namespace SpeakEasy
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

        public bool HasContent
        {
            get { return !string.IsNullOrEmpty(ContentType); }
        }

        public string ContentType
        {
            get { return response.ContentType; }
        }

        public string MediaType
        {
            get { return new ContentType(response.ContentType).MediaType; }
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