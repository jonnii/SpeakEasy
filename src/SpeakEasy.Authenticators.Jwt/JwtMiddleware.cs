using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Authenticators.Jwt
{
    public class JwtMiddleware : IHttpMiddleware
    {
        private readonly IJwtStrategy strategy;

        public JwtMiddleware(IJwtStrategy strategy)
        {
            this.strategy = strategy;
        }

        public IHttpMiddleware Next { get; set; }

        public virtual string TokenType { get; set; } = "Bearer";

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            var token = await strategy.GetToken(cancellationToken);

            request.AddHeader("Authorization", $"{TokenType} {token}");

            return await Next.Invoke(request, cancellationToken);
        }
    }
}
