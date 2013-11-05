using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// The transient cookie strategy returns a new cookie container on each request
    /// this is useful when you don't need to worry about session state and want each
    /// request to be independent
    /// </summary>
    public class TransientCookieStrategy : ICookieStrategy
    {
        public CookieContainer Get(IHttpRequest httpRequest)
        {
            return new CookieContainer();
        }
    }
}