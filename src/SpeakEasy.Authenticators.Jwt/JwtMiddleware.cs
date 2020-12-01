using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpeakEasy.Authenticators.Jwt
{
    public class JwtMiddleware : IHttpMiddleware
    {
        private readonly string tokenUrl;

        private readonly JwtMiddlewareSettings settings;

        private readonly IHttpClient client;

        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        private JwtSecurityToken current;

        public JwtMiddleware(string tokenUrl, JwtMiddlewareSettings settings)
        {
            this.tokenUrl = tokenUrl;
            this.settings = settings;

            client = HttpClient.Create(tokenUrl, new HttpClientSettings
            {
                Authenticator = settings.Authenticator
            });
        }

        public IHttpMiddleware Next { get; set; }

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            var token = await GetTokenAsync(cancellationToken);

            request.AddHeader("Authorization", $"Bearer {token}");

            return await Next.Invoke(request, cancellationToken);
        }

        private async Task<string> GetTokenAsync(CancellationToken cancellationToken)
        {
            if (current == null)
            {
                current = await RefreshTokenAsync(cancellationToken);
            }
            else if (current.IsExpired() && settings.RefreshOnExpiry)
            {
                current = await RefreshTokenAsync(cancellationToken);
            }

            return current.RawData;
        }

        private async Task<JwtSecurityToken> RefreshTokenAsync(CancellationToken cancellationToken)
        {
            var payload = await client.Get(string.Empty, null, cancellationToken)
                .OnOk()
                .AsString();

            if (settings.TokenLocator != null)
            {
                var value = settings.TokenLocator(payload);

                return new JwtSecurityToken(value);
            }

            var decoded = JsonConvert.DeserializeObject<JObject>(payload);
            var tokenValue = GetValidToken(decoded);

            var token = new JwtSecurityToken(tokenValue);

            return token;
        }

        private string GetValidToken(JObject decoded)
        {
            if (!string.IsNullOrEmpty(settings.JsonPathToToken))
            {
                return decoded.SelectToken(settings.JsonPathToToken)?.Value<string>();
            }

            foreach (var property in decoded)
            {
                var token = property.Value?.ToString();

                if (tokenHandler.CanReadToken(token))
                {
                    return token;
                }
            }

            return null;
        }
    }
}
