using System;
using System.IO;
using System.Net;

namespace SpeakEasy
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse(Uri requestUrl, HttpStatusCode httpStatusCode, Stream body, ISerializer deserializer)
        {
            Deserializer = deserializer;

            RequestedUrl = requestUrl;
            HttpStatusCode = httpStatusCode;
            Body = body;
        }

        public Uri RequestedUrl { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public Stream Body { get; private set; }

        public ISerializer Deserializer { get; private set; }

        public IHttpResponse On(HttpStatusCode code, Action action)
        {
            if (HttpStatusCode == code)
            {
                action();
            }

            return this;
        }

        public IHttpResponse On<T>(HttpStatusCode code, Action<T> action)
        {
            if (!Is(code))
            {
                var message = string.Format(
                    "Expected the status code to be {0} but was {1}", code, HttpStatusCode);

                throw new HttpException(message);
            }

            var deserialied = Deserializer.Deserialize<T>(Body);

            action(deserialied);

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
    }
}