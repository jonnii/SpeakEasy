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
        private readonly MiddlewareCollection middleware;

        private readonly IHttpMiddleware defaultMiddleware;

        private readonly Func<IHttpRequest, CancellationToken, Task<IHttpResponse>> middlewareHead;

        public RequestRunner(
            SystemHttpClient client,
            ITransmissionSettings transmissionSettings,
            IArrayFormatter arrayFormatter,
            CookieContainer cookieContainer,
            MiddlewareCollection middleware)
        {
            this.middleware = middleware;

            defaultMiddleware = new RequestMiddleware(
                transmissionSettings,
                arrayFormatter,
                cookieContainer,
                client);

            middleware.AppendMiddleware(defaultMiddleware);

            middlewareHead = middleware.BuildMiddlewareChain();
        }

        public Task<IHttpResponse> RunAsync(IHttpRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return middlewareHead.Invoke(request, cancellationToken);
        }
    }
}
