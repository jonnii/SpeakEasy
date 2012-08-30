using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SpeakEasy.Extensions;

namespace SpeakEasy
{
    public partial class RequestRunner : IRequestRunner
    {
        private const int DefaultBufferSize = 0x100;

        private readonly ITransmissionSettings transmissionSettings;

        private readonly IAuthenticator authenticator;

        private readonly Dictionary<string, Action<HttpWebRequest, string>> reservedHeaderApplicators =
            new Dictionary<string, Action<HttpWebRequest, string>>
            {
                {"Accept", (h, v) => h.Accept = v}
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

        public Task<IHttpResponse> RunAsync(IHttpRequest request)
        {
            var completionSource = new TaskCompletionSource<IHttpResponse>();

            RunWebRequestAsync(request, completionSource).Iterate(completionSource);

            return completionSource.Task;
        }

        public IEnumerable<Task> RunWebRequestAsync(IHttpRequest httpRequest, TaskCompletionSource<IHttpResponse> streamCompletionSource)
        {
            var webRequest = BuildWebRequest(httpRequest);

            var serializedBody = httpRequest.Body.Serialize(transmissionSettings);

            webRequest.ContentType = serializedBody.ContentType;

            if (serializedBody.HasContent)
            {
                var getRequestStream = Task.Factory.FromAsync<Stream>(webRequest.BeginGetRequestStream, webRequest.EndGetRequestStream, webRequest);

                yield return getRequestStream;

                using (var requestStream = getRequestStream.Result)
                {
                    yield return serializedBody.WriteTo(requestStream);
                }
            }
            else
            {
                if (serializedBody.ContentLength != -1)
                {
                    webRequest.ContentLength = serializedBody.ContentLength;
                }
            }

            var getResponse = Task.Factory.FromAsync<IHttpWebResponse>(webRequest.BeginGetResponse, GetWebResponse, webRequest);
            yield return getResponse;

            using (var response = getResponse.Result)
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var bufferSize = response.ContentLength > 0
                        ? response.ContentLength
                        : DefaultBufferSize;

                    var readResponseStream = responseStream.ReadStreamAsync(bufferSize);
                    yield return readResponseStream;

                    var webResponse = CreateHttpResponse(response, readResponseStream.Result);
                    streamCompletionSource.TrySetResult(webResponse);
                }
            }
        }

        partial void BuildWebRequestFrameworkSpecific(IHttpRequest httpRequest, HttpWebRequest webRequest);

        public HttpWebRequest BuildWebRequest(IHttpRequest httpRequest)
        {
            authenticator.Authenticate(httpRequest);

            var url = httpRequest.BuildRequestUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);
            request.Credentials = httpRequest.Credentials;
            request.Method = httpRequest.HttpMethod;
            request.AllowAutoRedirect = httpRequest.AllowAutoRedirect;
            request.CookieContainer = httpRequest.CookieContainer ?? new CookieContainer();

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

        private IHttpWebResponse GetWebResponse(IAsyncResult result)
        {
            var webRequest = (WebRequest)result.AsyncState;

            try
            {
                return new HttpWebResponseWrapper((HttpWebResponse)webRequest.EndGetResponse(result));
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