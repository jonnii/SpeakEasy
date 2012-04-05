using System;
using System.Net;

namespace Resticle
{
    public class RestResponse : IRestResponse
    {
        public RestResponse(Uri requestUrl)
        {
            RequestedUrl = requestUrl;
        }

        public Uri RequestedUrl { get; private set; }

        public IRestResponse On(HttpStatusCode code, Action action)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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