using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.IntegrationTests.Middleware
{
    public class ConsoleLoggingMiddleware : IHttpMiddleware
    {
        public IHttpMiddleware Next { get; set; }

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Before Request: {0}", request);
            var response = await Next.Invoke(request, cancellationToken).ConfigureAwait(false);
            Console.WriteLine("After Response: {0}", response);
            return response;
        }
    }
}
