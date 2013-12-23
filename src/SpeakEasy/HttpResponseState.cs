using System;
using System.Linq;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// The http response state contains the details of an http response, for example cookies and headers.
    /// </summary>
    public class HttpResponseState : IHttpResponseState
    {
        public HttpResponseState(
            HttpStatusCode statusCode,
            string statusDescription,
            Uri requestUrl,
            Header[] headers,
            Cookie[] cookies,
            string contentType,
             string server,
            string contentEncoding,
            DateTime lastModified)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            RequestUrl = requestUrl;
            Headers = headers;
            Cookies = cookies;
            ContentType = contentType;
            Server = server;
            ContentEncoding = contentEncoding;
            LastModified = lastModified;
        }

        public string Server { get; set; }

        public string ContentEncoding { get; set; }

        public DateTime LastModified { get; set; }

        public HttpStatusCode StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        public Uri RequestUrl { get; private set; }

        public Header[] Headers { get; private set; }

        public Cookie[] Cookies { get; private set; }

        public string ContentType { get; private set; }

        public Header GetHeader(string name)
        {
            var header = Headers.FirstOrDefault(h => h.Name == name.ToLowerInvariant());

            if (header != null)
            {
                return header;
            }

            var message = string.Format(
                "Could not find a header with the name {0}", name);

            throw new ArgumentException(message);
        }

        public string GetHeaderValue(string name)
        {
            var header = GetHeader(name);

            return header.Value;
        }
    }
}