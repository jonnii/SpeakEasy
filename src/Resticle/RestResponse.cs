using System;
using System.Net;

namespace Resticle
{
    public class RestResponse : IRestResponse
    {
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

        public IRestResponseHandler OnOK()
        {
            throw new NotImplementedException();
        }

        public IRestResponse OnOK(Action action)
        {
            throw new NotImplementedException();
        }

        public IRestResponse OnOK<T>(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public bool Is(HttpStatusCode code)
        {
            throw new NotImplementedException();
        }

        public bool IsOK()
        {
            throw new NotImplementedException();
        }
    }
}