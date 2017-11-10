using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace SpeakEasy
{
    /// <summary>
    /// The http response state contains the details of an http response, for example cookies and headers.
    /// </summary>
    public class HttpResponseState : IHttpResponseState
    {
        private readonly HttpContentHeaders headers;

        public HttpResponseState(
            HttpStatusCode statusCode,
            string statusDescription,
            Uri requestUrl,
            Cookie[] cookies,
            string contentType,
            string server,
            HttpContentHeaders headers)
        {
            this.headers = headers;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            RequestUrl = requestUrl;
            Cookies = cookies;
            ContentType = contentType;
            Server = server;
        }

        public string Server { get; }

        public string ContentEncoding => headers.ContentEncoding.ToString();

        public DateTime LastModified => headers.LastModified.GetValueOrDefault(DateTime.UtcNow).Date;

        public HttpStatusCode StatusCode { get; }

        public string StatusDescription { get; }

        public Uri RequestUrl { get; }

        public Cookie[] Cookies { get; }

        public string ContentType { get; }
    }
}
