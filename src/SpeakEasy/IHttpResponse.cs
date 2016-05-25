using System;
using System.IO;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// A chainable http response which gives you access to all the data available
    /// on a response to an http service
    /// </summary>
    public interface IHttpResponse
    {
        /// <summary>
        /// The http status code of this response
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The content type of the response
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// The state of this http response
        /// </summary>
        IHttpResponseState State { get; }

        ///// <summary>
        ///// The body of the response
        ///// </summary>
        //Stream Body { get; }

        T ConsumeBody<T>(Func<Stream, T> onBody);

        void ConsumeBody(Action<Stream> onBody);

        /// <summary>
        /// The deserializer that will be used to deserialize the response
        /// </summary>
        ISerializer Deserializer { get; }

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code.
        /// </summary>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse On(HttpStatusCode code, Action action);

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code.
        /// </summary>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse On(int code, Action action);

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code,
        /// the body will be deserialized automatically to the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the body as</typeparam>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse On<T>(HttpStatusCode code, Action<T> action);

        /// <summary>
        /// Executes the given action when the response state code matches the supplied status code.
        /// The action will be given the http response state of the result.
        /// </summary>
        /// <param name="code">The http status code on which to execute the callback</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse On(HttpStatusCode code, Action<IHttpResponseState> action);

        /// <summary>
        /// Executes the given action when the response state code matches the supplied status code.
        /// The action will be given the http response state of the result.
        /// </summary>
        /// <param name="code">The http status code on which to execute the callback</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse On(int code, Action<IHttpResponseState> action);

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code,
        /// the body will be deserialized automatically to the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the body as</typeparam>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse On<T>(int code, Action<T> action);

        /// <summary>
        /// Get a response handler when the response status code matches the supplied status code,
        /// if the status code does not match an exception will be thrown.
        /// </summary>
        /// <param name="code">The http status code we're expecting</param>
        /// <returns>A response handler giving access to the body of the response</returns>
        IHttpResponseHandler On(HttpStatusCode code);

        /// <summary>
        /// Get a response handler when the response status code matches the supplied status code,
        /// if the status code does not match an exception will be thrown.
        /// </summary>
        /// <param name="code">The http status code we're expecting</param>
        /// <returns>A response handler giving access to the body of the response</returns>
        IHttpResponseHandler On(int code);

        /// <summary>
        /// Gets a response handler when the response status code is OK (200). If the status code does not
        /// match OK (200) an exception will be thrown.
        /// </summary>
        /// <returns>A response handler giving access to the body of the response</returns>
        IHttpResponseHandler OnOk();

        /// <summary>
        /// Executes the given action when the response status code is OK (200).
        /// </summary>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse OnOk(Action action);

        /// <summary>
        /// Executes the given action when the response status code is OK (200). The body
        /// will be automatically deserialized to match the type of the callback.
        /// </summary>
        /// <typeparam name="T">The type of the callback</typeparam>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse OnOk<T>(Action<T> action);

        /// <summary>
        /// Indicates whether or not the response is of a given status code.
        /// </summary>
        /// <param name="code">The status code to check</param>
        /// <returns>True if the code matches the response status code</returns>
        bool Is(HttpStatusCode code);

        /// <summary>
        /// Indicates whether or not the response is of a given status code.
        /// </summary>
        /// <param name="code">The status code to check</param>
        /// <returns>True if the code matches the response status code</returns>
        bool Is(int code);

        /// <summary>
        /// Indicates whether or not this response is a OK (200).
        /// </summary>
        /// <returns>True if the response status code is OK (200)</returns>
        bool IsOk();

        /// <summary>
        /// Gets the the header with the given name
        /// </summary>
        /// <param name="name">The name of the header to get</param>
        /// <returns>The header</returns>
        Header GetHeader(string name);

        /// <summary>
        /// Gets the value of the header with the given name
        /// </summary>
        /// <param name="name">The name of the header to get</param>
        /// <returns>The header value</returns>
        string GetHeaderValue(string name);
    }
}
