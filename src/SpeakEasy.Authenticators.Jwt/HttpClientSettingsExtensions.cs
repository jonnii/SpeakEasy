using System;

namespace SpeakEasy.Authenticators.Jwt
{
    public static class HttpClientSettingsExtensions
    {
        public static void AddJwtMiddleware(this HttpClientSettings settings, string tokenUrl)
        {
            AddJwtMiddleware(settings, tokenUrl, _ => { });
        }

        public static void AddJwtMiddleware(this HttpClientSettings settings, string tokenUrl, IAuthenticator authenticator)
        {
            AddJwtMiddleware(settings, tokenUrl, x =>
            {
                x.Authenticator = authenticator;
            });
        }

        public static void AddJwtMiddleware(this HttpClientSettings settings, string tokenUrl, Action<JwtMiddlewareSettings> configure)
        {
            var jwtSettings = new JwtMiddlewareSettings();
            configure(jwtSettings);

            settings.Middleware.Append(new JwtMiddleware(tokenUrl, jwtSettings));
        }
    }
}
