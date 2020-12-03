using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpeakEasy.Authenticators.Jwt
{
    public class HttpJsonJwtStrategy : JwtStrategy
    {
        private readonly bool refreshOnExpiry;

        private readonly IHttpClient client;

        private JwtSecurityToken current;

        public HttpJsonJwtStrategy(string tokenUrl, IAuthenticator authenticator = null, bool refreshOnExpiry = true)
        {
            this.refreshOnExpiry = refreshOnExpiry;

            client = CreateClient(tokenUrl, authenticator);
        }

        public HttpJsonJwtStrategy(string tokenUrl, HttpClientSettings settings, bool refreshOnExpiry = true)
        {
            this.refreshOnExpiry = refreshOnExpiry;

            client = HttpClient.Create(tokenUrl, settings);
        }

        private IHttpClient CreateClient(string tokenUrl, IAuthenticator authenticator)
        {
            var settings = new HttpClientSettings();

            if (authenticator != null)
            {
                settings.Authenticator = authenticator;
            }

            return HttpClient.Create(tokenUrl, settings);
        }

        public override async Task<string> GetToken(CancellationToken cancellationToken = default)
        {
            if (current == null)
            {
                current = await GetLatestToken(cancellationToken);
            }
            else if (IsExpired(current) && refreshOnExpiry)
            {
                current = await GetLatestToken(cancellationToken);
            }

            return current.RawData;
        }

        protected virtual async Task<JwtSecurityToken> GetLatestToken(CancellationToken cancellationToken = default)
        {
            var payload = await client.Get(string.Empty, null, cancellationToken)
                .OnOk()
                .AsString();

            var decoded = JsonConvert.DeserializeObject<JObject>(payload);

            var token = LocateTokenInPayload(decoded);

            return new JwtSecurityToken(token);
        }

        protected virtual string LocateTokenInPayload(JObject decoded)
        {
            foreach (var property in decoded)
            {
                var token = property.Value?.ToString();

                if (CanReadToken(token))
                {
                    return token;
                }
            }

            return null;
        }
    }
}
