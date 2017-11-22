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
    public class DefaultRequestDispatchingMiddleware : IHttpMiddleware
    {
        private static readonly Cookie[] NoCookies = new Cookie[0];

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IArrayFormatter arrayFormatter;

        private readonly CookieContainer cookieContainer;

        private readonly System.Net.Http.HttpClient client;

        public DefaultRequestDispatchingMiddleware(
            ITransmissionSettings transmissionSettings,
            IArrayFormatter arrayFormatter,
            CookieContainer cookieContainer,
            System.Net.Http.HttpClient client)
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

            var httpRequest = BuildHttpRequestMessage(request);

            if (serializedBody.HasContent)
            {
                var memoryStream = new MemoryStream();
                await serializedBody.WriteToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
                memoryStream.Position = 0;

                httpRequest.Content = new StreamContent(memoryStream);
                httpRequest.Content.Headers.ContentLength = memoryStream.Length;
                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(serializedBody.ContentType);
            }

            var httpResponse = await client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            var responseStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

            return CreateHttpResponse(
                httpRequest,
                httpResponse,
                responseStream);
        }

        public HttpRequestMessage BuildHttpRequestMessage(IHttpRequest httpRequest)
        {
            var message = new HttpRequestMessage(
                httpRequest.HttpMethod,
                httpRequest.BuildRequestUrl(arrayFormatter));

            // untyped headers
            foreach (var header in httpRequest.Headers)
            {
                message.Headers.Add(header.Name, header.Value);
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
                httpResponse.StatusCode,
                httpResponse.ReasonPhrase,
                httpResponse.RequestMessage.RequestUri,
                Enumerable.ToArray<Cookie>(cookies),
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
