using System.Net.Http;
using System.Net.Http.Headers;

namespace SpeakEasy.Authenticators
{
    public class BearerAuthentication : IAuthenticator
    {
        private readonly string token;

        public BearerAuthentication(string token)
        {
            this.token = token;
        }

        public void Authenticate(HttpClientHandler httpClientHandler)
        {
        }

        public void Authenticate(System.Net.Http.HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
