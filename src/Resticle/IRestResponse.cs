using System;
using System.Net;

namespace Resticle
{
    public interface IRestResponse
    {
        void On(HttpStatusCode code, Action action);

        IRestResponseHandler On(HttpStatusCode code);

        IRestResponseHandler OnOK();
    }
}