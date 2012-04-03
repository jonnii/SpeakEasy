using System;
using System.Net;

namespace Resticle
{
    public class RestResponse : IRestResponse
    {
        public void On(HttpStatusCode code, Action action)
        {
            action();
        }

        public IRestResponseHandler On(HttpStatusCode code)
        {
            return new RestResponseHandler();
        }

        public IRestResponseHandler OnOK()
        {
            throw new NotImplementedException();
        }
    }
}