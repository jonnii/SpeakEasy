using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Middleware;

namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
        private readonly IAuthenticator authenticator;

        private readonly CookieContainer cookieContainer;

        private readonly IUserAgent userAgent;

        private readonly List<IHttpMiddleware> middleware = new List<IHttpMiddleware>();

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IAuthenticator authenticator,
            IArrayFormatter arrayFormatter,
            CookieContainer cookieContainer,
            IUserAgent userAgent)
        {
            this.authenticator = authenticator;
            this.cookieContainer = cookieContainer;
            this.userAgent = userAgent;

            var client = BuildClient();

            var defaultMiddleware = new DefaultRequestDispatchingMiddleware(
                transmissionSettings,
                arrayFormatter,
                cookieContainer,
                client);

            middleware.Add(defaultMiddleware);
        }

        public System.Net.Http.HttpClient BuildClient()
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseDefaultCredentials = false,
                CookieContainer = cookieContainer,
                //Credentials = httpRequest.Credentials,
            };

            authenticator.Authenticate(handler);

            // handler.AllowAutoRedirect = httpRequest.AllowAutoRedirect;
            // handler.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);

            // BuildWebRequestFrameworkSpecific(httpRequest, handler);

            // foreach (var header in httpRequest.Headers)
            // {
            //     ApplyHeaderToRequest(header, handler);
            // }

            var httpClient = new System.Net.Http.HttpClient(handler);

            authenticator.Authenticate(httpClient);

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent.Name);

            return httpClient;
        }

        public Task<IHttpResponse> RunAsync(IHttpRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var head = BuildMiddlewareChain();

            return head.Invoke(request, cancellationToken);
        }

        private Func<IHttpRequest, CancellationToken, Task<IHttpResponse>> BuildMiddlewareChain()
        {
            var head = middleware[0];

            for (var i = 0; i < middleware.Count; ++i)
            {
                var current = middleware[i];

                if (i < middleware.Count - 1)
                {
                    current.Next = middleware[i + 1];
                }
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
