using System;
using System.Net;

namespace Resticle
{
    /// <summary>
    /// A simple wrapper around an HttpWebResponse
    /// </summary>
    public interface IHttpWebResponse
    {
        Uri ResponseUri { get; }

        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Reads the body of the http web response
        /// </summary>
        /// <returns>A string representation of the body</returns>
        string ReadBody();
    }
}