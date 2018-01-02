using System;
using System.Net;
using System.Net.Http;

namespace SpeakEasy
{
    /// <summary>
    /// The http response state contains the details of an http response, for example cookies and headers.
    /// </summary>
    public class HttpResponseState : IHttpResponseState
    {
        private readonly HttpResponseMessage httpResponseMessage;

        public HttpResponseState(
            HttpResponseMessage httpResponseMessage,
            Cookie[] cookies,
            string contentType)
        {
            this.httpResponseMessage = httpResponseMessage;
            Cookies = cookies;
            ContentType = contentType;
        }

        public string Server => httpResponseMessage.Headers.Server.ToString();

        public string ContentEncoding => httpResponseMessage.Content.Headers.ContentEncoding.ToString();

        public DateTime LastModified => httpResponseMessage.Content.Headers.LastModified.GetValueOrDefault(DateTime.UtcNow).Date;

        public HttpStatusCode StatusCode => httpResponseMessage.StatusCode;

        public string ReasonPhrase => httpResponseMessage.ReasonPhrase;

        public Uri RequestUrl => httpResponseMessage.RequestMessage.RequestUri;

        public Cookie[] Cookies { get; }

        public string ContentType { get; }

        public void Dispose()
        {
            httpResponseMessage.Dispose();
        }
    }
}
