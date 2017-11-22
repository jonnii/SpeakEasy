using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
        /// The credentials on this http request
        /// </summary>
        ICredentials Credentials { get; set; }

        /// <summary>
        /// Indicates whether or not this http request should allow auto redirects
        /// </summary>
        bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// Builds the method specific request url
        /// </summary>
        /// <returns>A url</returns>
        string BuildRequestUrl(IArrayFormatter arrayFormatter);

        void AddHeader(string header, string value);

        void AddHeader(Action<HttpRequestHeaders> headers);

        List<Action<HttpRequestHeaders>> Headers { get; }
    }
}
