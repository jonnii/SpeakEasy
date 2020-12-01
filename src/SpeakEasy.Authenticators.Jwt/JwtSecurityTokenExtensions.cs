using System;
using System.IdentityModel.Tokens.Jwt;

namespace SpeakEasy.Authenticators.Jwt
{
    internal static class JwtSecurityTokenExtensions
    {
        public static bool IsExpired(this JwtSecurityToken token)
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
    }
}
