using System;
using System.IO;
using System.Linq;
using System.Net;

namespace SpeakEasy
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse(ISerializer deserializer, Stream body, HttpStatusCode httpStatusCode, Uri requestUrl, Header[] headers, string contentType)
        {
            Deserializer = deserializer;

            RequestedUrl = requestUrl;
            HttpStatusCode = httpStatusCode;
            Headers = headers;
            ContentType = contentType;
            Body = body;
        }

        public Uri RequestedUrl { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public Header[] Headers { get; private set; }

        public string ContentType { get; private set; }

        public Stream Body { get; private set; }

        public ISerializer Deserializer { get; private set; }

        public IHttpResponse On(HttpStatusCode code, Action action)
        {
            if (Is(code))
            {
                action();
            }

            return this;
        }

        public IHttpResponse On<T>(HttpStatusCode code, Action<T> action)
        {
            if (Is(code))
            {
                var deserialied = Deserializer.Deserialize<T>(Body);
                action(deserialied);
            }

            return this;
        }

        public IHttpResponseHandler On(HttpStatusCode code)
        {
            return new HttpResponseHandler(this);
        }

        public IHttpResponseHandler OnOk()
        {
            if (!IsOk())
            {
                var message = string.Format(
                    "Cannot get an http response handler for Ok, because the status was {0}", HttpStatusCode);

                throw new HttpException(message);
            }

            return new HttpResponseHandler(this);
        }

        public IHttpResponse OnOk(Action action)
        {
            return On(HttpStatusCode.OK, action);
        }

        public IHttpResponse OnOk<T>(Action<T> action)
        {
            return On(HttpStatusCode, action);
        }

        public bool Is(HttpStatusCode code)
        {
            return HttpStatusCode == code;
        }

        public bool IsOk()
        {
            return Is(HttpStatusCode.OK);
        }

        public Header GetHeader(string name)
        {
            var header = Headers.FirstOrDefault(h => h.Name == name.ToLowerInvariant());

            if (header == null)
            {
                var message = string.Format(
                    "Could not find a header with the name {0}", name);

                throw new ArgumentException(message);
            }

            return header;
        }

        public string GetHeaderValue(string name)
        {
            var header = GetHeader(name);

            return header.Value;
        }

        public override string ToString()
        {
            return string.Format(
                "[HttpResponse StatusCode={0}, ContentType={1}, RequestedUrl={2}]",
                HttpStatusCode,
                ContentType,
                RequestedUrl);
        }
    }
}