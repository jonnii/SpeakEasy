using System.Net.Http;

namespace SpeakEasy.Authenticators
{
    public class NullAuthenticator : IAuthenticator
    {
        public void Authenticate(HttpClientHandler httpClientHandler)
        {
        }

        public void Authenticate(System.Net.Http.HttpClient httpClient)
        {
        }
    }
}
