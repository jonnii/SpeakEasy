using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// The persistent cookie strategy uses the same cookie container on each request
    /// </summary>
    public class PersistentCookieStrategy : ICookieStrategy
    {
        private readonly CookieContainer container;

        public PersistentCookieStrategy()
            : this(new CookieContainer())
        {
        }

        public PersistentCookieStrategy(CookieContainer container)
        {
            this.container = container;
        }

        public CookieContainer Get(IHttpRequest httpRequest)
        {
            return container;
        }
    }
}
