using System;
using System.IO;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// A simple wrapper around an HttpWebResponse
    /// </summary>
    public interface IHttpWebResponse : IDisposable
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
        /// The headers that came back as part of this response
        /// </summary>
        Header[] Headers { get; }

        /// <summary>
        /// The cookies that came back as part of this response
        /// </summary>
        Cookie[] Cookies { get; }

        /// <summary>
        /// Indicates whether or not the web response has content
        /// </summary>
        bool HasContent { get; }

        /// <summary>
        /// The content type of the web response
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// The content length of the web response
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// Gets the response stream
        /// </summary>
        /// <returns>The response stream</returns>
        Stream GetResponseStream();
    }
}