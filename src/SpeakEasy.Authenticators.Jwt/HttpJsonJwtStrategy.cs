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

        public HttpJsonJwtStrategy(string tokenUrl, HttpClientSettings settings, bool refreshOnExpiry = true)
        {
            this.refreshOnExpiry = refreshOnExpiry;

            client = HttpClient.Create(tokenUrl, settings);
        }

        public HttpJsonJwtStrategy(IHttpClient client, bool refreshOnExpiry = true)
        {
            this.client = client;
            this.refreshOnExpiry = refreshOnExpiry;
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
