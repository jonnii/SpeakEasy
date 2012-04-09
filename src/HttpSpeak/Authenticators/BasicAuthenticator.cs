using System;
using System.Text;

namespace HttpSpeak.Authenticators
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

        public void Authenticate(IHttpRequest httpRequest)
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Concat(username, ":", password)));
            var authorizationHeader = string.Concat("Basic ", token);

            httpRequest.AddHeader("Authorization", authorizationHeader);
        }
    }
}
