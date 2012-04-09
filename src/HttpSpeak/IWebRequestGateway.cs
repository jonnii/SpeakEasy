using System;
using System.Net;

namespace HttpSpeak
{
    /// <summary>
    /// The web request gateway is responsible for sending a web request
    /// and building a suitable http web response
    /// </summary>
    public interface IWebRequestGateway
    {
        /// <summary>
        /// Sends a web request
        /// </summary>
        /// <param name="webRequest">The web request to send</param>
        /// <param name="responseConverter">A function that turns a http web response into a T</param>
        /// <typeparam name="T">The type of the response</typeparam>
        /// <returns>The result of the response converter</returns>
        T Send<T>(WebRequest webRequest, Func<IHttpWebResponse, T> responseConverter);
    }
}