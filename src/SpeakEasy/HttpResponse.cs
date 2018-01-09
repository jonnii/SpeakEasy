using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace SpeakEasy
{
    internal class HttpResponse : IHttpResponse
    {
        private readonly ISerializer deserializer;

        private readonly SingleUseStream body;

        public HttpResponse(
            ISerializer deserializer,
            Stream body,
            IHttpResponseState state,
            HttpContentHeaders headers)
        {
            this.deserializer = deserializer;
            this.body = new SingleUseStream(body);

            State = state;
            Headers = headers;
        }

        public HttpContentHeaders Headers { get; }

        public IHttpResponseState State { get; }

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
            if (!Is(code))
            {
                return this;
            }

            var deserialied = deserializer.Deserialize<T>(body.GetAndConsumeStream());
            action(deserialied);

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

            return new HttpResponseHandler(this, deserializer, body);
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

            return new HttpResponseHandler(this, deserializer, body);
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

        public void Dispose()
        {
            body.Dispose();
            State.Dispose();
        }
    }
}
