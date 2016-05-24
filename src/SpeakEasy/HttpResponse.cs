using System;
using System.IO;
using System.Net;

namespace SpeakEasy
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse(
            ISerializer deserializer,
            Stream body,
            IHttpResponseState state)
        {
            Deserializer = deserializer;
            Body = body;
            State = state;
        }

        public IHttpResponseState State { get; }

        public Stream Body { get; }

        public ISerializer Deserializer { get; }

        public string ContentType => State.ContentType;

        public HttpStatusCode StatusCode => State.StatusCode;

        public IHttpResponse On(HttpStatusCode code, Action action)
        {
            if (Is(code))
            {
                action();
            }

            return this;
        }

        public IHttpResponse On(int code, Action action)
        {
            return On((HttpStatusCode)code, action);
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

        public IHttpResponse On(HttpStatusCode code, Action<IHttpResponseState> action)
        {
            if (Is(code))
            {
                action(State);
            }

            return this;
        }

        public IHttpResponse On(int code, Action<IHttpResponseState> action)
        {
            return On((HttpStatusCode)code, action);
        }

        public IHttpResponse On<T>(int code, Action<T> action)
        {
            return On((HttpStatusCode)code, action);
        }

        public IHttpResponseHandler On(HttpStatusCode code)
        {
            if (!Is(code))
            {
                OnIncorrectStatusCode(code);
            }

            return new HttpResponseHandler(this);
        }

        public IHttpResponseHandler On(int code)
        {
            return On((HttpStatusCode)code);
        }

        public IHttpResponseHandler OnOk()
        {
            if (!IsOk())
            {
                OnIncorrectStatusCode(HttpStatusCode.OK);
            }

            return new HttpResponseHandler(this);
        }

        private void OnIncorrectStatusCode(HttpStatusCode expected)
        {
            throw new HttpException($"Cannot get an http response handler for {expected}, because the status was {StatusCode}", this);
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

        public bool Is(int code)
        {
            return Is((HttpStatusCode)code);
        }

        public bool IsOk()
        {
            return Is(HttpStatusCode.OK);
        }

        public Header GetHeader(string name)
        {
            return State.GetHeader(name);
        }

        public string GetHeaderValue(string name)
        {
            return State.GetHeaderValue(name);
        }

        public override string ToString()
        {
            return $"[HttpResponse StatusCode={StatusCode}, ContentType={ContentType}, RequestUrl={State.RequestUrl}]";
        }
    }
}
