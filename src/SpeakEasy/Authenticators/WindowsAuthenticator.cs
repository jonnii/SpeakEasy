using System.Net;
using System.Net.Http;

namespace SpeakEasy.Authenticators
{
    /// <summary>
    /// The windows authenticator will authenticate
    /// an http request with windows credentials
    /// </summary>
    public class WindowsAuthenticator : IAuthenticator
    {
        private readonly ICredentials credentials;

        /// <summary>
        /// Creates a windows authenticator with the default credentials
        /// </summary>
        public WindowsAuthenticator()
            : this(CredentialCache.DefaultCredentials)
        {
        }

        /// <summary>
        /// Creates a windows authenticator with a specific username
        /// and password
        /// </summary>
        /// <param name="username">The username to authenticate with</param>
        /// <param name="password">The password to authenticate with</param>
        public WindowsAuthenticator(string username, string password)
            : this(new NetworkCredential(username, password))
        {
        }

        /// <summary>
        /// Creates a windows authenticator with a specific credentials
        /// </summary>
        /// <param name="credentials">The credentials to authenticate with</param>
        public WindowsAuthenticator(ICredentials credentials)
        {
            this.credentials = credentials;
        }

        public void Authenticate(HttpClientHandler httpClientHandler)
        {
            httpClientHandler.Credentials = credentials;
        }

        public void Authenticate(System.Net.Http.HttpClient httpClient)
        {
        }
    }
}
