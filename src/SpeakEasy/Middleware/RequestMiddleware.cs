using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Middleware
{
    using SystemHttpClient = System.Net.Http.HttpClient;

    internal class RequestMiddleware : IHttpMiddleware
    {
        private static readonly Cookie[] NoCookies = new Cookie[0];

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IQuerySerializer arrayFormatter;

        private readonly CookieContainer cookieContainer;

        private readonly SystemHttpClient client;

        public RequestMiddleware(
            SystemHttpClient client,
            ITransmissionSettings transmissionSettings,
            IQuerySerializer arrayFormatter,
            CookieContainer cookieContainer)
        {
            this.transmissionSettings = transmissionSettings;
            this.arrayFormatter = arrayFormatter;
            this.cookieContainer = cookieContainer;
            this.client = client;
        }

        public IHttpMiddleware Next
        {
            get => throw new NotSupportedException("The default request dispatching middlware cannot have a next step");
            set => throw new NotSupportedException("The default request dispatching middlware cannot have a next step");
        }

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            var serializedBody = request.Body.Serialize(transmissionSettings, arrayFormatter);

            using (var httpRequest = BuildHttpRequestMessage(request))
            {
                await serializedBody.WriteTo(httpRequest, cancellationToken).ConfigureAwait(false);

                var httpResponse = await client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
                {
                    var responseStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    return CreateHttpResponse(
                        httpRequest,
                        httpResponse,
                        responseStream);
                }
            }
        }

        public HttpRequestMessage BuildHttpRequestMessage(IHttpRequest httpRequest)
        {
            var message = new HttpRequestMessage(
                httpRequest.HttpMethod,
                httpRequest.BuildRequestUrl(arrayFormatter));

            foreach (var mediaType in transmissionSettings.DeserializableMediaTypes)
            {
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            }

            foreach (var header in httpRequest.Headers)
            {
                header(message.Headers);
            }

            return message;
        }

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
                httpResponse,
                cookies,
                contentType);

            return new HttpResponse(
                deserializer,
                body,
                state,
                httpResponse.Content.Headers);
        }
    }
}
