#if FRAMEWORK

using System;
using System.Net;

namespace SpeakEasy
{
    public partial class HttpResponseState
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
            : this(statusCode, statusDescription, requestUrl, headers, cookies, contentType)
        {
            Server = server;
            ContentEncoding = contentEncoding;
            LastModified = lastModified;
        }

        public string Server { get; set; }

        public string ContentEncoding { get; set; }

        public DateTime LastModified { get; set; }
    }
}

#endif