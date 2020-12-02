namespace SpeakEasy.Authenticators.Jwt
{
    public static class HttpClientSettingsExtensions
    {
        public static void AddJwtMiddleware(this HttpClientSettings settings, IJwtStrategy strategy)
        {
            settings.Middleware.Append(new JwtMiddleware(strategy));
        }
    }
}
