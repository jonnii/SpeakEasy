using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Authenticators.Jwt
{
    public interface IJwtStrategy
    {
        Task<string> GetToken(CancellationToken cancellationToken = default);
    }
}
