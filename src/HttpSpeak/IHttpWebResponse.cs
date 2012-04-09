using System;
using System.Net;

namespace HttpSpeak
{
    /// <summary>
    /// A simple wrapper around an HttpWebResponse
    /// </summary>
    public interface IHttpWebResponse
    {
        /// <summary>
        /// The uri that responded to the web request
        /// </summary>
        Uri ResponseUri { get; }

        /// <summary>
        /// The status code of the web response
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Indicates whether or not the web response has content
        /// </summary>
        bool HasContent { get; }

        /// <summary>
        /// The content type of the web response
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// The media type of the web response
        /// </summary>
        string MediaType { get; }

        /// <summary>
        /// Reads the body of the http web response
        /// </summary>
        /// <returns>A string representation of the body</returns>
        string ReadBody();
    }
}