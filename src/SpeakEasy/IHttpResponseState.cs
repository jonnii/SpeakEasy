using System;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// An IHttpResponseState contains all the response state from an http endpoint.
    /// </summary>
    public interface IHttpResponseState
    {
        Header[] Headers { get; }

        Cookie[] Cookies { get; }

        Uri RequestUrl { get; }

        string Server { get; }

        string ContentType { get; }

        string ContentEncoding { get; }

        string StatusDescription { get; }

        DateTime LastModified { get; }

        HttpStatusCode StatusCode { get; }

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