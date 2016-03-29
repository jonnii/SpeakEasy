using System;
using System.Net;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public static class HttpResponseTaskExtensions
    {
        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> On(this Task<IHttpResponse> response, HttpStatusCode code, Action action)
        {
            return (await response).On(code, action);
        }

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> On(this Task<IHttpResponse> response, int code, Action action)
        {
            return (await response).On(code, action);
        }

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code,
        /// the body will be deserialized automatically to the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the body as</typeparam>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> On<T>(this Task<IHttpResponse> response, HttpStatusCode code, Action<T> action)
        {
            return (await response).On(code, action);
        }

        /// <summary>
        /// Executes the given action when the response state code matches the supplied status code.
        /// The action will be given the http response state of the result.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code on which to execute the callback</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> On(this Task<IHttpResponse> response, HttpStatusCode code, Action<IHttpResponseState> action)
        {
            return (await response).On(code, action);
        }

        /// <summary>
        /// Executes the given action when the response state code matches the supplied status code.
        /// The action will be given the http response state of the result.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code on which to execute the callback</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> On(this Task<IHttpResponse> response, int code, Action<IHttpResponseState> action)
        {
            return (await response).On(code, action);
        }

        /// <summary>
        /// Executes the given action when the response status code matches the supplied status code,
        /// the body will be deserialized automatically to the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the body as</typeparam>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code we're expecting</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> On<T>(this Task<IHttpResponse> response, int code, Action<T> action)
        {
            return (await response).On(code, action);
        }

        /// <summary>
        /// Get a response handler when the response status code matches the supplied status code,
        /// if the status code does not match an exception will be thrown.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code we're expecting</param>
        /// <returns>A response handler giving access to the body of the response</returns>
        public static async Task<IHttpResponseHandler> On(this Task<IHttpResponse> response, HttpStatusCode code)
        {
            return (await response).On(code);
        }

        /// <summary>
        /// Get a response handler when the response status code matches the supplied status code,
        /// if the status code does not match an exception will be thrown.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The http status code we're expecting</param>
        /// <returns>A response handler giving access to the body of the response</returns>
        public static async Task<IHttpResponseHandler> On(this Task<IHttpResponse> response, int code)
        {
            return (await response).On(code);
        }

        /// <summary>
        /// Gets a response handler when the response status code is OK (200). If the status code does not
        /// match OK (200) an exception will be thrown.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <returns>A response handler giving access to the body of the response</returns>
        public static async Task<IHttpResponseHandler> OnOk(this Task<IHttpResponse> response)
        {
            return (await response).OnOk();
        }

        /// <summary>
        /// Executes the given action when the response status code is OK (200).
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> OnOk(this Task<IHttpResponse> response, Action action)
        {
            return (await response).OnOk(action);
        }

        /// <summary>
        /// Executes the given action when the response status code is OK (200). The body
        /// will be automatically deserialized to match the type of the callback.
        /// </summary>
        /// <typeparam name="T">The type of the callback</typeparam>
        /// <param name="response">The http response</param>
        /// <param name="action">An action callback</param>
        /// <returns>A chainable http response</returns>
        public static async Task<IHttpResponse> OnOk<T>(this Task<IHttpResponse> response, Action<T> action)
        {
            return (await response).OnOk(action);
        }

        /// <summary>
        /// Indicates whether or not the response is of a given status code.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The status code to check</param>
        /// <returns>True if the code matches the response status code</returns>
        public static async Task<bool> Is(this Task<IHttpResponse> response, HttpStatusCode code)
        {
            return (await response).Is(code);
        }

        /// <summary>
        /// Indicates whether or not the response is of a given status code.
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="code">The status code to check</param>
        /// <returns>True if the code matches the response status code</returns>
        public static async Task<bool> Is(this Task<IHttpResponse> response, int code)
        {
            return (await response).Is(code);
        }

        /// <summary>
        /// Indicates whether or not this response is a OK (200).
        /// </summary>
        /// <param name="response">The http response</param>
        /// <returns>True if the response status code is OK (200)</returns>
        public static async Task<bool> IsOk(this Task<IHttpResponse> response)
        {
            return (await response).IsOk();
        }

        /// <summary>
        /// Gets the the header with the given name
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="name">The name of the header to get</param>
        /// <returns>The header</returns>
        public static async Task<Header> GetHeader(this Task<IHttpResponse> response, string name)
        {
            return (await response).GetHeader(name);
        }

        /// <summary>
        /// Gets the value of the header with the given name
        /// </summary>
        /// <param name="response">The http response</param>
        /// <param name="name">The name of the header to get</param>
        /// <returns>The header value</returns>
        public static async Task<string> GetHeaderValue(this Task<IHttpResponse> response, string name)
        {
            return (await response).GetHeaderValue(name);
        }
    }
}
