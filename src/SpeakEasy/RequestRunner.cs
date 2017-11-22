using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Middleware;

namespace SpeakEasy
{
    using SystemHttpClient = System.Net.Http.HttpClient;

    public class RequestRunner : IRequestRunner
    {
        private readonly List<IHttpMiddleware> middleware;

        private readonly IHttpMiddleware defaultMiddleware;

        private readonly Func<IHttpRequest, CancellationToken, Task<IHttpResponse>> middlewareHead;

        public RequestRunner(
            SystemHttpClient client,
            ITransmissionSettings transmissionSettings,
            IArrayFormatter arrayFormatter,
            CookieContainer cookieContainer,
            List<IHttpMiddleware> middleware)
        {
            this.middleware = middleware;

            defaultMiddleware = new RequestMiddleware(
                transmissionSettings,
                arrayFormatter,
                cookieContainer,
                client);

            middlewareHead = BuildMiddlewareChain();
        }

        public Task<IHttpResponse> RunAsync(IHttpRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return middlewareHead.Invoke(request, cancellationToken);
        }

        private Func<IHttpRequest, CancellationToken, Task<IHttpResponse>> BuildMiddlewareChain()
        {
            if (!middleware.Any())
            {
                return defaultMiddleware.Invoke;
            }

            var head = middleware[0];

            for (var i = 0; i < middleware.Count; ++i)
            {
                var current = middleware[i];

                current.Next = i < middleware.Count - 1
                    ? middleware[i + 1]
                    : defaultMiddleware;
            }

            return head.Invoke;
        }
    }
}
