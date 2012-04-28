#if FRAMEWORK

using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace SpeakEasy
{
    /// <summary>
    /// The framework specific properties for an http request
    /// </summary>
    public partial interface IHttpRequest
    {
        /// <summary>
        /// The x509 certificates associated with this http request
        /// </summary>
        X509CertificateCollection ClientCertificates { get; }

        /// <summary>
        /// The web proxy to use when making this http request
        /// </summary>
        IWebProxy Proxy { get; set; }
    }
}

#endif