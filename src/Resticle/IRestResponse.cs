using System;
using System.Net;

namespace Resticle
{
    public interface IRestResponse
    {
        IRestResponse On(HttpStatusCode code, Action action);

        IRestResponse On<T>(HttpStatusCode code, Action<T> action);

        IRestResponseHandler On(HttpStatusCode code);

        IRestResponseHandler OnOK();

        IRestResponse OnOK(Action action);

        IRestResponse OnOK<T>(Action<T> action);

        bool Is(HttpStatusCode code);

        bool IsOK();
    }
}