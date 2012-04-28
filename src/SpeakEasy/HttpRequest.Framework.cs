#if FRAMEWORK

using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace SpeakEasy
{
    public abstract partial class HttpRequest
    {
        public X509CertificateCollection ClientCertificates { get; set; }

        public IWebProxy Proxy { get; set; }
    }
}

#endif