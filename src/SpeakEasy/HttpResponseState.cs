using System;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// The http response state contains the details of an http response, for example cookies and headers.
    /// </summary>
    public class HttpResponseState
    {
        public HttpResponseState(
            HttpStatusCode statusCode,
            Uri requestUrl,
            Header[] headers,
            Cookie[] cookies,
            string contentType)
        {
            StatusCode = statusCode;
            RequestUrl = requestUrl;
            Headers = headers;
            Cookies = cookies;
            ContentType = contentType;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public Uri RequestUrl { get; private set; }

        public Header[] Headers { get; private set; }

        public Cookie[] Cookies { get; private set; }

        public string ContentType { get; private set; }
    }
}