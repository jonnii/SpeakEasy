#if FRAMEWORK

using System.Net;

namespace SpeakEasy
{
    public partial class RequestRunner
    {
        partial void BuildWebRequestFrameworkSpecific(IHttpRequest httpRequest, HttpWebRequest webRequest)
        {
            ServicePointManager.Expect100Continue = false;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

            if (httpRequest.ClientCertificates != null)
            {
                webRequest.ClientCertificates = httpRequest.ClientCertificates;
            }

            webRequest.Proxy = httpRequest.Proxy;

            if (httpRequest.AllowAutoRedirect && httpRequest.MaximumAutomaticRedirections != null)
            {
                webRequest.MaximumAutomaticRedirections = httpRequest.MaximumAutomaticRedirections.Value;
            }
        }
    }
}

#endif
