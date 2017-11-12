using System.Collections.Generic;
using System.Net;
using System.Net.Http;

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
        /// The http method for this request
        /// </summary>
        HttpMethod HttpMethod { get; }

        /// <summary>
        /// The body of this request
        /// </summary>
        IRequestBody Body { get; }

        /// <summary>
        /// The user agent of this http request
        /// </summary>
        IUserAgent UserAgent { get; set; }

        /// <summary>
        /// The web proxy to use when making this http request
        /// </summary>
        // IWebProxy Proxy { get; set; }

        /// <summary>
        /// The x509 certificates associated with this http request
        /// </summary>
        // X509CertificateCollection ClientCertificates { get; }

        /// <summary>
        /// The maximum number of automatic redirections when allow auto redirect it set to true
        /// </summary>
        int? MaximumAutomaticRedirections { get; set; }

        /// <summary>
        /// Indicates that this http request has a user agent
        /// </summary>
        bool HasUserAgent { get; }

        /// <summary>
        /// The number of headers on this http request
        /// </summary>
        int NumHeaders { get; }

        /// <summary>
        /// Gets the headers attached to this http request
        /// </summary>
        IEnumerable<Header> Headers { get; }

        /// <summary>
        /// The credentials on this http request
        /// </summary>
        ICredentials Credentials { get; set; }

        /// <summary>
        /// Indicates whether or not this http request should allow auto redirects
        /// </summary>
        bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// The cookie container to use when making this http request
        /// </summary>
        CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Adds a header to this http request
        /// </summary>
        /// <param name="name">The name of the http header</param>
        /// <param name="value">The value of the header</param>
        void AddHeader(string name, string value);

        /// <summary>
        /// Builds the method specific request url
        /// </summary>
        /// <returns>A url</returns>
        string BuildRequestUrl(IArrayFormatter arrayFormatter);
    }
}
