using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// A cookie container provider is responsible for selecting a cookie container
    /// for a given request
    /// </summary>
    public interface ICookieStrategy
    {
        /// <summary>
        /// Gets a cookie container for a given http request
        /// </summary>
        CookieContainer Get(IHttpRequest httpRequest);
    }
}