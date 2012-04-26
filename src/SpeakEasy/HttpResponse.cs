using System;
using System.IO;
using System.Linq;
using System.Net;

namespace SpeakEasy
{
    internal class HttpResponse : IHttpResponse
    {
        public HttpResponse(
            ISerializer deserializer,
            Stream body,
            HttpResponseState state)
        {
            Deserializer = deserializer;
            Body = body;
            State = state;
        }

        public HttpResponseState State { get; private set; }

        public Stream Body { get; private set; }

        public ISerializer Deserializer { get; private set; }

        public string ContentType
        {
            get { return State.ContentType; }
        }

        public HttpStatusCode StatusCode
        {
            get { return State.StatusCode; }
        }

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
                    "Cannot get an http response handler for Ok, because the status was {0}", StatusCode);

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
            return On(StatusCode, action);
        }

        public bool Is(HttpStatusCode code)
        {
            return StatusCode == code;
        }

        public bool IsOk()
        {
            return Is(HttpStatusCode.OK);
        }

        public Header GetHeader(string name)
        {
            var header = State.Headers.FirstOrDefault(h => h.Name == name.ToLowerInvariant());

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
                "[HttpResponse StatusCode={0}, ContentType={1}, RequestUrl={2}]",
                StatusCode,
                ContentType,
                State.RequestUrl);
        }
    }
}