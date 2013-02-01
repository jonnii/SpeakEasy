using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SpeakEasy.Extensions;

namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
        private const int DefaultBufferSize = 0x100;

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IAuthenticator authenticator;

        private readonly Dictionary<string, Action<HttpWebRequest, string>> reservedHeaderApplicators =
            new Dictionary<string, Action<HttpWebRequest, string>>
            {
                {"Accept", (h, v) => h.Accept = v},
            };

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IAuthenticator authenticator)
        {
            this.transmissionSettings = transmissionSettings;
            this.authenticator = authenticator;
        }

        public IHttpResponse Run(IHttpRequest request)
        {
            try
            {
                var task = RunAsync(request);
                task.Wait();
                return task.Result;
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        public async Task<IHttpResponse> RunAsync(IHttpRequest request)
        {
            using (var httpRequest = BuildWebRequest(request))
            {
                var serializedBody = request.Body.Serialize(transmissionSettings);

                httpRequest.Headers.Add("ContentType", serializedBody.ContentType);

                if (serializedBody.HasContent)
                {
                    var stream = new MemoryStream();
                    await serializedBody.WriteTo(stream);
                    httpRequest.Content = new StreamContent(stream);
                }

                var client = new System.Net.Http.HttpClient();
                using (var response = await client.SendAsync(httpRequest))
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var wrappedResponse = GetWebResponse(response);

                    var localStream = new MemoryStream();
                    await responseStream.CopyToAsync(localStream);
                    localStream.Position = 0;

                    return CreateHttpResponse(wrappedResponse, localStream);
                }
            }
        }

        public HttpRequestMessage BuildWebRequest(IHttpRequest httpRequest)
        {
            authenticator.Authenticate(httpRequest);

            var request = new HttpRequestMessage(
                GetHttpMethod(httpRequest.HttpMethod),
                httpRequest.BuildRequestUrl());

            request.Headers.Add(
                "Accept",
                string.Join(", ", transmissionSettings.DeserializableMediaTypes));

            //request.UseDefaultCredentials = false;
            //request.Credentials = httpRequest.Credentials;
            //request.Method = httpRequest.HttpMethod;
            //request.CookieContainer = httpRequest.CookieContainer ?? new CookieContainer();

            //foreach (var header in httpRequest.Headers)
            //{
            //    ApplyHeaderToRequest(header, request);
            //}

            return request;
        }

        private HttpMethod GetHttpMethod(string httpMethod)
        {
            switch (httpMethod)
            {
                case "GET":
                    return HttpMethod.Get;
                case "DELETE":
                    return HttpMethod.Delete;
                case "HEAD":
                    return HttpMethod.Head;
                case "OPTIONS":
                    return HttpMethod.Options;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                default:
                    throw new NotSupportedException();
            }
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

        private IHttpWebResponse GetWebResponse(HttpResponseMessage result)
        {
            return new HttpWebResponseWrapper(result);
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