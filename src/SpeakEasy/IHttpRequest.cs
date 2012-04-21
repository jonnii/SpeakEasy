using System.Collections.Generic;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// An http request represents one http interaction with
    /// a web service that speaks http
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// The resource that will be requested by this http request
        /// </summary>
        Resource Resource { get; }

        /// <summary>
        /// The user agent of this http request
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// The number of headers on this http request
        /// </summary>
        int NumHeaders { get; }

        /// <summary>
        /// Gets the headers attached to this http request
        /// </summary>
        IEnumerable<Header> Headers { get; }

        /// <summary>
        /// The windows credentials on this http request
        /// </summary>
        ICredentials Credentials { get; set; }

        string HttpMethod { get; }

        IRequestBody Body { get; }

        /// <summary>
        /// Adds a header to this http request
        /// </summary>
        /// <param name="name">The name of the http header</param>
        /// <param name="value">The value of the header</param>
        void AddHeader(string name, string value);

        string BuildRequestUrl();
    }
}