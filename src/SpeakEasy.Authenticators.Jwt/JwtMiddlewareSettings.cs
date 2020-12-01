using System;

namespace SpeakEasy.Authenticators.Jwt
{
    public class JwtMiddlewareSettings
    {
        public bool RefreshOnExpiry { get; set; }

        public IAuthenticator Authenticator { get; set; } = new NullAuthenticator();

        public string JsonPathToToken { get; set; }

        public Func<string, string> TokenLocator { get; set; }
    }
}
