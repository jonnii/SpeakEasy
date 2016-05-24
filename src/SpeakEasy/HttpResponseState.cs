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

        public string Server { get; }

        public string ContentEncoding { get; }

        public DateTime LastModified { get; }

        public HttpStatusCode StatusCode { get; }

        public string StatusDescription { get; }

        public Uri RequestUrl { get; }

        public Header[] Headers { get; }

        public Cookie[] Cookies { get; }

        public string ContentType { get; }

        public Header GetHeader(string name)
        {
            var header = Headers.FirstOrDefault(h => h.Name == name.ToLowerInvariant());

            if (header != null)
            {
                return header;
            }

            throw new ArgumentException($"Could not find a header with the name {name}");
        }

        public string GetHeaderValue(string name)
        {
            var header = GetHeader(name);

            return header.Value;
        }
    }
}
