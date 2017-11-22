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

        public RequestRunner(
            SystemHttpClient client,
            ITransmissionSettings transmissionSettings,
            IArrayFormatter arrayFormatter,
            CookieContainer cookieContainer,
            List<IHttpMiddleware> middleware)
        {
            this.middleware = middleware;

            defaultMiddleware = new DefaultRequestDispatchingMiddleware(
                transmissionSettings,
                arrayFormatter,
                cookieContainer,
                client);
        }

        public Task<IHttpResponse> RunAsync(IHttpRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var head = BuildMiddlewareChain();

            return head.Invoke(request, cancellationToken);
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

        // private void BuildWebRequestFrameworkSpecific(IHttpRequest httpRequest, HttpWebRequest webRequest)
        // {
        //     // ServicePointManager.Expect100Continue = false;
        //     webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

        //     if (httpRequest.ClientCertificates != null)
        //     {
        //         webRequest.ClientCertificates = httpRequest.ClientCertificates;
        //     }

        //     if (httpRequest.Proxy != null)
        //     {
        //         webRequest.Proxy = httpRequest.Proxy;
        //     }

        //     if (httpRequest.AllowAutoRedirect && httpRequest.MaximumAutomaticRedirections != null)
        //     {
        //         webRequest.MaximumAutomaticRedirections = httpRequest.MaximumAutomaticRedirections.Value;
        //     }
        // }
    }
}
