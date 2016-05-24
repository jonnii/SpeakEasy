using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
        private const int DefaultBufferSize = 0x100;

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IAuthenticator authenticator;

        private readonly ICookieStrategy cookieStrategy;

        private readonly IArrayFormatter arrayFormatter;

        private readonly Dictionary<string, Action<HttpWebRequest, string>> reservedHeaderApplicators =
            new Dictionary<string, Action<HttpWebRequest, string>>
            {
                {"Accept", (h, v) => h.Accept = v}
            };

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IAuthenticator authenticator,
            ICookieStrategy cookieStrategy,
            IArrayFormatter arrayFormatter)
        {
            this.transmissionSettings = transmissionSettings;
            this.authenticator = authenticator;
            this.cookieStrategy = cookieStrategy;
            this.arrayFormatter = arrayFormatter;
        }

        public async Task<IHttpResponse> RunAsync(IHttpRequest httpRequest)
        {
            var webRequest = BuildWebRequest(httpRequest);
            var serializedBody = httpRequest.Body.Serialize(transmissionSettings, arrayFormatter);
            webRequest.ContentType = serializedBody.ContentType;

            if (serializedBody.HasContent)
            {
                var requestStream = await webRequest.GetRequestStreamAsync();

                using (requestStream)
                {
                    await serializedBody.WriteToAsync(requestStream);
                }
            }
            else
            {
                if (serializedBody.ContentLength != -1)
                {
                    webRequest.ContentLength = serializedBody.ContentLength;
                }
            }

            using (var response = await GetResponseWrapper(webRequest))
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var bufferSize = response.ContentLength > 0
                        ? response.ContentLength
                        : DefaultBufferSize;

                    var readResponseStream = new MemoryStream();
                    await responseStream.CopyToAsync(readResponseStream, (int)bufferSize);

                    readResponseStream.Position = 0;

                    return CreateHttpResponse(response, readResponseStream);
                }
            }
        }

        private async Task<HttpWebResponseWrapper> GetResponseWrapper(WebRequest webRequest)
        {
            try
            {
                var response = await webRequest.GetResponseAsync();
                return new HttpWebResponseWrapper((HttpWebResponse)response);
            }
            catch (WebException wex)
            {
                var innerResponse = wex.Response;
                if (innerResponse != null)
                {
                    return new HttpWebResponseWrapper((HttpWebResponse)innerResponse);
                }

                throw;
            }
        }

        private void BuildWebRequestFrameworkSpecific(IHttpRequest httpRequest, HttpWebRequest webRequest)
        {
            ServicePointManager.Expect100Continue = false;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

            if (httpRequest.ClientCertificates != null)
            {
                webRequest.ClientCertificates = httpRequest.ClientCertificates;
            }

            if (httpRequest.Proxy != null)
            {
                webRequest.Proxy = httpRequest.Proxy;
            }

            if (httpRequest.AllowAutoRedirect && httpRequest.MaximumAutomaticRedirections != null)
            {
                webRequest.MaximumAutomaticRedirections = httpRequest.MaximumAutomaticRedirections.Value;
            }
        }

        public HttpWebRequest BuildWebRequest(IHttpRequest httpRequest)
        {
            authenticator.Authenticate(httpRequest);

            var url = httpRequest.BuildRequestUrl(arrayFormatter);

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);
            request.Credentials = httpRequest.Credentials;
            request.Method = httpRequest.HttpMethod;
            request.AllowAutoRedirect = httpRequest.AllowAutoRedirect;
            request.CookieContainer = httpRequest.CookieContainer ?? cookieStrategy.Get(httpRequest);

            if (httpRequest.HasUserAgent)
            {
                request.UserAgent = httpRequest.UserAgent.Name;
            }

            BuildWebRequestFrameworkSpecific(httpRequest, request);

            foreach (var header in httpRequest.Headers)
            {
                ApplyHeaderToRequest(header, request);
            }

            return request;
        }

        private void ApplyHeaderToRequest(Header header, HttpWebRequest request)
        {
            var headerName = header.Name;

            if (reservedHeaderApplicators.ContainsKey(headerName))
            {
                reservedHeaderApplicators[headerName](request, header.Value);
            }
            else
            {
                request.Headers[header.Name] = header.Value;
            }
        }

        public IHttpResponse CreateHttpResponse(IHttpWebResponse webResponse, Stream body)
        {
            var deserializer = transmissionSettings.FindSerializer(webResponse.ContentType);

            return new HttpResponse(
                deserializer,
                body,
                webResponse.BuildState());
        }
    }
}
