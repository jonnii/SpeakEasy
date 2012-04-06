using System;
using System.Net;

namespace Resticle
{
    public class RestResponse : IRestResponse
    {
        private readonly IDeserializer deserializer;

        public RestResponse(
            IDeserializer deserializer,
            Uri requestUrl,
            HttpStatusCode httpStatusCode,
            string body)
        {
            this.deserializer = deserializer;

            RequestedUrl = requestUrl;
            HttpStatusCode = httpStatusCode;
            Body = body;
        }

        public Uri RequestedUrl { get; private set; }

        public string Body { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public IRestResponse On(HttpStatusCode code, Action action)
        {
            if (HttpStatusCode == code)
            {
                action();
            }

            return this;
        }

        public IRestResponse On<T>(HttpStatusCode code, Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IRestResponseHandler On(HttpStatusCode code)
        {
            return new RestResponseHandler(this, deserializer);
        }

        public IRestResponseHandler OnOk()
        {
            if (!IsOk())
            {
                var message = string.Format(
                    "Cannot get a rest response handler for Ok, because the status was {0}", HttpStatusCode);

                throw new RestException(message);
            }

            return new RestResponseHandler(this, deserializer);
        }

        public IRestResponse OnOk(Action action)
        {
            return On(HttpStatusCode.OK, action);
        }

        public IRestResponse OnOk<T>(Action<T> action)
        {
            throw new NotImplementedException();
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