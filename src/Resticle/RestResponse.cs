using System;
using System.Net;

namespace Resticle
{
    public class RestResponse : IRestResponse
    {
        public RestResponse(Uri requestUrl, HttpStatusCode httpStatusCode)
        {
            RequestedUrl = requestUrl;
            HttpStatusCode = httpStatusCode;
        }

        public Uri RequestedUrl { get; private set; }

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
            return new RestResponseHandler();
        }

        public IRestResponseHandler OnOk()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool IsOk()
        {
            throw new NotImplementedException();
        }
    }
}