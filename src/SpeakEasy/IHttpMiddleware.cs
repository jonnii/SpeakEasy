using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public interface IHttpMiddleware
    {
        IHttpMiddleware Next { get; set; }

        Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken);
    }
}
