using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Middleware;

namespace SpeakEasy
{
    using SystemHttpClient = System.Net.Http.HttpClient;

    internal class RequestRunner : IRequestRunner
    {
        private readonly Func<IHttpRequest, CancellationToken, Task<IHttpResponse>> middlewareHead;

        public RequestRunner(
            SystemHttpClient client,
            ITransmissionSettings transmissionSettings,
            IQuerySerializer querySerializer,
            CookieContainer cookieContainer,
            MiddlewareCollection middleware)
        {
            var defaultMiddleware = new RequestMiddleware(client, transmissionSettings, querySerializer, cookieContainer);
            middleware.Append(defaultMiddleware);
            middlewareHead = middleware.BuildMiddlewareChain();
        }

        public Task<IHttpResponse> RunAsync(IHttpRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return middlewareHead.Invoke(request, cancellationToken);
        }
    }
}
