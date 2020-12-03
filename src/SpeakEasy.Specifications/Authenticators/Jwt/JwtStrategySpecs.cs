using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;
using Microsoft.IdentityModel.Tokens;
using SpeakEasy.Authenticators.Jwt;

namespace SpeakEasy.Specifications.Authenticators.Jwt
{
    [Subject(typeof(JwtStrategy))]
    class JwtStrategySpecs
    {
        static TestJwtStrategy strategy;

        static string token;

        Establish context = () =>
            strategy = new TestJwtStrategy();

        class when_validating_token
        {
            static bool valid;

            Establish context = () =>
                token = GenerateToken(DateTime.UtcNow.AddMinutes(5));

            Because of = () =>
                valid = strategy.GetIsValid(token);

            It should_be_valid = () =>
                valid.ShouldBeTrue();
        }

        class when_decoding_token
        {
            static JwtSecurityToken token;

            Establish context = () =>
                token = new JwtSecurityToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            It should_have_no_expiry = () =>
                token.ValidTo.ShouldEqual(DateTime.MinValue);
        }

        class when_expiry_is_in_the_future
        {
            static bool expired;

            Establish context = () =>
                token = GenerateToken(DateTime.UtcNow.AddMinutes(5));

            Because of = () =>
                expired = strategy.GetIsExpired(new JwtSecurityToken(token));

            It should_not_be_expired = () =>
                expired.ShouldBeFalse();
        }

        class when_expiry_is_in_the_past
        {
            static bool expired;

            Establish context = () =>
                token = GenerateToken(DateTime.UtcNow.AddMinutes(-5));

            Because of = () =>
                expired = strategy.GetIsExpired(new JwtSecurityToken(token));

            It should_be_expired = () =>
                expired.ShouldBeTrue();
        }

        static string GenerateToken(DateTime to)
        {
            var symmetricKey = Convert.FromBase64String("really long secret name that is used to generate jwt token");
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.MinValue,
                NotBefore = DateTime.MinValue,
                Expires = to,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        class TestJwtStrategy : JwtStrategy
        {
            public bool GetIsValid(string token)
            {
                return CanReadToken(token);
            }

            public bool GetIsExpired(JwtSecurityToken token)
            {
                return IsExpired(token);
            }

            public override Task<string> GetToken(CancellationToken cancellationToken = default)
            {
                return Task.FromResult(string.Empty);
            }
        }
    }
}
