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
            var now = DateTime.UtcNow;

            if (token.ValidFrom != DateTime.MinValue && token.ValidFrom < now)
            {
                return false;
            }

            if (token.ValidTo != DateTime.MinValue && token.ValidTo > now)
            {
                return false;
            }

            return true;
        }

        public abstract Task<string> GetToken(CancellationToken cancellationToken = default);
    }
}
