using System;
using System.Net;

namespace Resticle
{
    /// <summary>
    /// A chainable rest response which gives you access to all the data available
    /// on a response to a restful service.
    /// </summary>
    public interface IRestResponse
    {
        IRestResponse On(HttpStatusCode code, Action action);

        IRestResponse On<T>(HttpStatusCode code, Action<T> action);

        IRestResponseHandler On(HttpStatusCode code);

        IRestResponseHandler OnOk();

        IRestResponse OnOk(Action action);

        IRestResponse OnOk<T>(Action<T> action);

        bool Is(HttpStatusCode code);

        bool IsOk();
    }
}