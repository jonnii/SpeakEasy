using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Authenticators.Jwt
{
    public abstract class JwtStrategy : IJwtStrategy
    {
        protected JwtSecurityTokenHandler TokenHandler { get; } = new JwtSecurityTokenHandler();

        protected virtual bool CanReadToken(string token)
        {
            return TokenHandler.CanReadToken(token);
        }

        protected virtual bool IsExpired(JwtSecurityToken token)
        {
            return token.ValidTo != DateTime.MinValue && token.ValidTo < DateTime.UtcNow;
        }

        public abstract Task<string> GetToken(CancellationToken cancellationToken = default);
    }
}
