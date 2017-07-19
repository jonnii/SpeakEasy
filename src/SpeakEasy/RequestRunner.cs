using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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

        // private readonly Dictionary<string, Action<HttpWebRequest, string>> reservedHeaderApplicators =
        //     new Dictionary<string, Action<HttpWebRequest, string>>
        //     {
        //         {"Accept", (h, v) => h.Accept = v}
        //     };

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
            var webRequest = BuildClient(httpRequest);

            var serializedBody = httpRequest.Body.Serialize(transmissionSettings, arrayFormatter);
            
            var message = BuildHttpRequestMessage(httpRequest);

            if(serializedBody.HasContent) 
            {
                var memoryStream = new MemoryStream();
                await serializedBody.WriteToAsync(memoryStream);
                memoryStream.Position = 0;
                
                message.Content = new StreamContent(memoryStream);
                message.Content.Headers.ContentLength = memoryStream.Length;
                message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(serializedBody.ContentType);
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

            //var bufferSize = response.ContentLength > 0
            //    ? response.ContentLength
            //    : DefaultBufferSize;

            var response = await webRequest.SendAsync(message);

            var readResponseStream = new MemoryStream();
            var responseStream = await response.Content.ReadAsStreamAsync();

            await responseStream.CopyToAsync(readResponseStream, DefaultBufferSize).ConfigureAwait(false);
            readResponseStream.Position = 0;

            return CreateHttpResponse(response, readResponseStream);

            //using (var response = await GetResponseWrapper(webRequest).ConfigureAwait(false))
            //{
            //    using (var responseStream = response.GetResponseStream())
            //    {

            //        var readResponseStream = new MemoryStream();
            //        await responseStream.CopyToAsync(readResponseStream, (int)bufferSize).ConfigureAwait(false);

            //        readResponseStream.Position = 0;

            //        return CreateHttpResponse(response, readResponseStream);
            //    }
            //}
        }

        public HttpRequestMessage BuildHttpRequestMessage(IHttpRequest httpRequest)
        {
            var method = GetMethod(httpRequest.HttpMethod); 
            var url = httpRequest.BuildRequestUrl(arrayFormatter);

            return new HttpRequestMessage(
                method, 
                url);
        }

        private HttpMethod GetMethod(string method)
        {
            return new HttpMethod(method);
        }

        // private async Task<HttpWebResponseWrapper> GetResponseWrapper(WebRequest webRequest)
        // {
        //     try
        //     {
        //         var response = await webRequest.GetResponseAsync().ConfigureAwait(false);
        //         return new HttpWebResponseWrapper((HttpWebResponse)response);
        //     }
        //     catch (WebException wex)
        //     {
        //         var innerResponse = wex.Response;
        //         if (innerResponse != null)
        //         {
        //             return new HttpWebResponseWrapper((HttpWebResponse)innerResponse);
        //         }

        //         throw;
        //     }
        // }

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

        public System.Net.Http.HttpClient BuildClient(IHttpRequest httpRequest)
        {
            authenticator.Authenticate(httpRequest);

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            };

            handler.UseDefaultCredentials = false;
            handler.Credentials = httpRequest.Credentials;
            // handler.AllowAutoRedirect = httpRequest.AllowAutoRedirect;
            handler.CookieContainer = httpRequest.CookieContainer ?? cookieStrategy.Get(httpRequest);

            //handler.Method = httpRequest.HttpMethod;
            //handler.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);

            if (httpRequest.HasUserAgent)
            {
                //handler.UserAgent = httpRequest.UserAgent.Name;
            }

            //BuildWebRequestFrameworkSpecific(httpRequest, handler);

            // foreach (var header in httpRequest.Headers)
            // {
            //     ApplyHeaderToRequest(header, handler);
            // }

            var client = new System.Net.Http.HttpClient(handler);

            return client;
        }

        private void ApplyHeaderToRequest(Header header, HttpRequestMessage request)
        {
            var headerName = header.Name;

            // if (reservedHeaderApplicators.ContainsKey(headerName))
            // {
            //     reservedHeaderApplicators[headerName](request, header.Value);
            // }
            // else
            {
                request.Headers.Add(header.Name, new []{header.Value});

                // request.Headers[header.Name] = header.Value;
            }
        }

        public IHttpResponse CreateHttpResponse(HttpResponseMessage webResponse, Stream body)
        {
            if (webResponse == null)
            {
                throw new ArgumentNullException(nameof(webResponse));
            }
            
            var end = new StreamReader(body).ReadToEnd();

            var contentType = webResponse.Content.Headers.ContentType.MediaType.ToString();

            var deserializer = transmissionSettings.FindSerializer(contentType);

            var state = new HttpResponseState(
                webResponse.StatusCode,
                webResponse.ReasonPhrase,
                webResponse.RequestMessage.RequestUri,
                new Header[0],
                new Cookie[0],
                contentType,
                webResponse.Headers.Server.ToString(),
                webResponse.Content.Headers.ContentEncoding.ToString(),
                webResponse.Content.Headers.LastModified.GetValueOrDefault(DateTime.UtcNow).Date);

            return new HttpResponse(
                deserializer,
                body,
                state);
        }
    }
}
