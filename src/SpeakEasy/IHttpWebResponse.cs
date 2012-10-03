using System;
using System.IO;

namespace SpeakEasy
{
    /// <summary>
    /// A simple wrapper around an HttpWebResponse
    /// </summary>
    internal interface IHttpWebResponse : IDisposable
    {
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

        /// <summary>
        /// Builds the http response state from this http web response
        /// </summary>
        /// <returns>An http response state</returns>
        HttpResponseState BuildState();
    }
}