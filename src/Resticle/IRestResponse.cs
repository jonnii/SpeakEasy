using System;
using System.Net;

namespace Resticle
{
    /// <summary>
    /// A chainable rest response which gives you access to all the data available
    /// on a response to a restful service
    /// </summary>
    public interface IRestResponse
    {
        /// <summary>
        /// The uri that was requested
        /// </summary>
        Uri RequestedUri { get; }

        /// <summary>
        /// Executes the given action when the rest response status code matches the supplied status code.
        /// </summary>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse On(HttpStatusCode code, Action action);

        /// <summary>
        /// Executes the given action when the rest response status code matches the supplied status code,
        /// the body will be deserialized automatically to the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the body as</typeparam>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse On<T>(HttpStatusCode code, Action<T> action);

        /// <summary>
        /// Get a rest response handler when the rest response status code matches the supplied status code,
        /// if the status code does not match an exception will be thrown.
        /// </summary>
        /// <param name="code">The http status code we're expecting</param>
        /// <returns>A rest response handler giving access to the body of the rest response</returns>
        IRestResponseHandler On(HttpStatusCode code);

        /// <summary>
        /// Gets a rest response handler when the rest response status code is OK (200). If the status code does not
        /// match OK (200) an exception will be thrown.
        /// </summary>
        /// <returns>A rest response handler giving access to the body of the rest response</returns>
        IRestResponseHandler OnOk();

        /// <summary>
        /// Executes the given action when the rest response status code is OK (200).
        /// </summary>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse OnOk(Action action);

        /// <summary>
        /// Executes the given action when the response status code is OK (200). The body
        /// will be automatically deserialized to match the type of the callback.
        /// </summary>
        /// <typeparam name="T">The type of the callback</typeparam>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse OnOk<T>(Action<T> action);

        /// <summary>
        /// Indicates whether or not the rest response is of a given status code.
        /// </summary>
        /// <param name="code">The status code to check</param>
        /// <returns>True if the code matches the rest response status code</returns>
        bool Is(HttpStatusCode code);

        /// <summary>
        /// Indicates whether or not this rest response is a OK (200).
        /// </summary>
        /// <returns>True if the response status code is OK (200)</returns>
        bool IsOk();
    }
}