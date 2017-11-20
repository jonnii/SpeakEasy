using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SpeakEasy.Authenticators
{
    public class BasicAuthenticator : IAuthenticator
    {
        private readonly string username;

        private readonly string password;

        public BasicAuthenticator(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void Authenticate(HttpClientHandler httpClientHandler)
        {
        }

        public void Authenticate(System.Net.Http.HttpClient httpClient)
        {
            var headerValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Concat(username, ":", password)));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
        }
    }
}
