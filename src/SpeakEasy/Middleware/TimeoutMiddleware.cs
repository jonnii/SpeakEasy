using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Middleware
{
    public class TimeoutMiddleware : IHttpMiddleware
    {
        public IHttpMiddleware Next { get; set; }

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await Next.Invoke(request, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                throw new TimeoutException();
            }
        }
    }
}
