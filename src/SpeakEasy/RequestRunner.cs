using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SpeakEasy.Extensions;

namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
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

            var getResponse = Task.Factory.FromAsync<WebResponse>(webRequest.BeginGetResponse, Thing, webRequest);
            yield return getResponse;

            using (var response = (HttpWebResponse)getResponse.Result)
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var output = new MemoryStream();
                    var buffer = new byte[response.ContentLength > 0 ? response.ContentLength : 0x100];
                    while (true)
                    {
                        var read = Task<int>.Factory.FromAsync(responseStream.BeginRead, responseStream.EndRead, buffer, 0, buffer.Length, null);
                        yield return read;
                        if (read.Result == 0)
                        {
                            break;
                        }
                        output.Write(buffer, 0, read.Result);
                    }

                    output.Seek(0, SeekOrigin.Begin);

                    var webResponse = CreateHttpResponse(response, output);
                    streamCompletionSource.TrySetResult(webResponse);
                }
            }
        }

        private WebResponse Thing(IAsyncResult result)
        {
            var webRequest = (WebRequest)result.AsyncState;

            try
            {
                return webRequest.EndGetResponse(result);
            }
            catch (WebException wex)
            {
                var innerResponse = wex.Response;
                if (innerResponse != null)
                {
                    return innerResponse;
                }

                throw;
            }
        }

        public IHttpResponse CreateHttpResponse(HttpWebResponse webResponse, Stream body)
        {
            var deserializer = transmissionSettings.FindSerializer(webResponse.ContentType);

            var headerNames = webResponse.Headers.AllKeys;
            var headers = headerNames.Select(n => new Header(n.ToLowerInvariant(), webResponse.Headers[n])).ToArray();

            return new HttpResponse(
                deserializer,
                body,
                webResponse.StatusCode,
                webResponse.ResponseUri,
                headers,
                webResponse.ContentType);
        }

        public HttpWebRequest BuildWebRequest(IHttpRequest httpRequest)
        {
            authenticator.Authenticate(httpRequest);

            var url = httpRequest.BuildRequestUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            ServicePointManager.Expect100Continue = false;

            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.Credentials = httpRequest.Credentials;
            request.Method = httpRequest.HttpMethod;

            if (!string.IsNullOrEmpty(httpRequest.UserAgent))
            {
                request.UserAgent = httpRequest.UserAgent;
            }

            foreach (var header in httpRequest.Headers)
            {
                request.Headers.Add(header.Name, header.Value);
            }

            return request;
        }
    }
}