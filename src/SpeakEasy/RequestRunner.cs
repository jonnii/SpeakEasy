using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
        private const int DefaultBufferSize = 0x100;

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IAuthenticator authenticator;

        private readonly IArrayFormatter arrayFormatter;

        private readonly System.Net.Http.HttpClient client;

        private readonly CookieContainer cookieContainer;

        // private readonly Dictionary<string, Action<HttpWebRequest, string>> reservedHeaderApplicators =
        //     new Dictionary<string, Action<HttpWebRequest, string>>
        //     {
        //         {"Accept", (h, v) => h.Accept = v}
        //     };

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IAuthenticator authenticator,
            IArrayFormatter arrayFormatter,
            CookieContainer cookieContainer)
        {
            this.transmissionSettings = transmissionSettings;
            this.authenticator = authenticator;
            this.arrayFormatter = arrayFormatter;
            this.cookieContainer = cookieContainer;

            client = BuildClient();
        }

        public System.Net.Http.HttpClient BuildClient()
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseDefaultCredentials = false,
                CookieContainer = cookieContainer
                //Credentials = httpRequest.Credentials,
            };

            // handler.AllowAutoRedirect = httpRequest.AllowAutoRedirect;
            // handler.Method = httpRequest.HttpMethod;
            // handler.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);

            //if (httpRequest.HasUserAgent)
            {
                //handler.UserAgent = httpRequest.UserAgent.Name;
            }

            // BuildWebRequestFrameworkSpecific(httpRequest, handler);

            // foreach (var header in httpRequest.Headers)
            // {
            //     ApplyHeaderToRequest(header, handler);
            // }

            return new System.Net.Http.HttpClient(handler);
        }

        public async Task<IHttpResponse> RunAsync(IHttpRequest tt, CancellationToken cancellationToken = default(CancellationToken))
        {
            authenticator.Authenticate(tt);

            var serializedBody = tt.Body.Serialize(transmissionSettings, arrayFormatter);

            var httpRequest = BuildHttpRequestMessage(tt);

            if (serializedBody.HasContent)
            {
                var memoryStream = new MemoryStream();
                await serializedBody.WriteToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
                memoryStream.Position = 0;

                httpRequest.Content = new StreamContent(memoryStream);
                httpRequest.Content.Headers.ContentLength = memoryStream.Length;
                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(serializedBody.ContentType);
            }

            //if (serializedBody.HasContent)
            //{
            //    var requestStream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false);

            //    using (requestStream)
            //    {
            //        await serializedBody.WriteToAsync(requestStream).ConfigureAwait(false);
            //    }
            //}
            //else
            //{
            //    if (serializedBody.ContentLength != -1)
            //    {
            //        webRequest.ContentLength = serializedBody.ContentLength;
            //    }
            //}

            var httpResponse = await client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            var responseStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

            return CreateHttpResponse(
                httpRequest,
                httpResponse,
                responseStream);
        }

        public HttpRequestMessage BuildHttpRequestMessage(IHttpRequest httpRequest)
        {
            return new HttpRequestMessage(
                httpRequest.HttpMethod,
                httpRequest.BuildRequestUrl(arrayFormatter));
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


        private void ApplyHeaderToRequest(Header header, HttpRequestMessage request)
        {
            var headerName = header.Name;

            // if (reservedHeaderApplicators.ContainsKey(headerName))
            // {
            //     reservedHeaderApplicators[headerName](request, header.Value);
            // }
            // else
            {
                request.Headers.Add(header.Name, new[] { header.Value });

                // request.Headers[header.Name] = header.Value;
            }
        }

        private static readonly Cookie[] NoCookies = new Cookie[0];

        public IHttpResponse CreateHttpResponse(HttpRequestMessage httpRequest, HttpResponseMessage httpResponse, Stream body)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException(nameof(httpRequest));
            }

            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            if (httpResponse.Content == null)
            {
                throw new ArgumentNullException(nameof(httpResponse.Content));
            }

            var contentType = httpResponse.Content?.Headers?.ContentType?.MediaType ?? "application/json";

            var deserializer = transmissionSettings.FindSerializer(contentType);

            var cookieCollection = cookieContainer.GetCookies(httpRequest.RequestUri);

            var cookies = cookieCollection.Count == 0
                ? NoCookies
                : cookieCollection.Cast<Cookie>().ToArray();

            var state = new HttpResponseState(
                httpResponse.StatusCode,
                httpResponse.ReasonPhrase,
                httpResponse.RequestMessage.RequestUri,
                cookies.ToArray(),
                contentType,
                httpResponse.Headers.Server.ToString(),
                httpResponse.Content.Headers);

            return new HttpResponse(
                deserializer,
                body,
                state,
                httpResponse.Content.Headers);
        }
    }
}
