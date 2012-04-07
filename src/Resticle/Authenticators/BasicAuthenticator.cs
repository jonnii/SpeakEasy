namespace Resticle.Authenticators
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

        public void Authenticate(IRestRequest restRequest)
        {
        }
    }
}
