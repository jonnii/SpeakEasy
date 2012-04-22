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
            if (serializedBody.ContentLength != -1)
            {
                webRequest.ContentLength = serializedBody.ContentLength;
            }

            if (serializedBody.HasContent)
            {
                var getRequestStream = Task.Factory.FromAsync<Stream>(webRequest.BeginGetRequestStream, webRequest.EndGetRequestStream, webRequest);

                yield return getRequestStream;

                using (var requestStream = getRequestStream.Result)
                {
                    yield return serializedBody.WriteTo(requestStream);
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

        partial void BuildWebRequestFrameworkSpecific(HttpWebRequest webRequest);

        public HttpWebRequest BuildWebRequest(IHttpRequest httpRequest)
        {
            authenticator.Authenticate(httpRequest);

            var url = httpRequest.BuildRequestUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);
            request.Credentials = httpRequest.Credentials;
            request.Method = httpRequest.HttpMethod;

            if (!string.IsNullOrEmpty(httpRequest.UserAgent))
            {
                request.UserAgent = httpRequest.UserAgent;
            }

            foreach (var header in httpRequest.Headers)
            {
                request.Headers[header.Name] = header.Value;
            }

            BuildWebRequestFrameworkSpecific(request);

            return request;
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
                webResponse.StatusCode,
                webResponse.ResponseUri,
                webResponse.Headers,
                webResponse.ContentType);
        }
    }
}