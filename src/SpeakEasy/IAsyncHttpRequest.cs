using System;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// An asynchronous http request
    /// </summary>
    public interface IAsyncHttpRequest
    {
        /// <summary>
        /// Registers a complete handler which is called when the async http response has completed
        /// </summary>
        /// <param name="handler">The handler to call when the request is completed</param>
        /// <returns>A chainable async http request</returns>
        IAsyncHttpRequest OnComplete(Action<IHttpResponse> handler);

        /// <summary>
        /// Starts the asynchronous http request
        /// </summary>
        /// <returns>A task handle</returns>
        Task Start();
    }
}