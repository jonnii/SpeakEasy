using System.Net.Http;

namespace SpeakEasy
{
    /// <summary>
    /// An authenticator is responsible for applying an authentication scheme to an http request, this usually 
    /// entails setting the appropriate http headers and/or cookies.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Apply the authentication scheme to the http request
        /// </summary>
        /// <param name="httpClientHandler">The http request to authenticate</param>
        void Authenticate(HttpClientHandler httpClientHandler);

        void Authenticate(System.Net.Http.HttpClient httpClient);
    }
}
