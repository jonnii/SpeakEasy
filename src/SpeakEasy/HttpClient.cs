using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Bodies;
using SpeakEasy.Requests;

namespace SpeakEasy
{
    public class HttpClient : IHttpClient
    {
        /// <summary>
        /// Creates a new http client with default settings
        /// </summary>
        /// <param name="rootUrl">The root url that all requests will be relative to</param>
        /// <returns>A new http client</returns>
        public static IHttpClient Create(string rootUrl)
        {
            return Create(rootUrl, new HttpClientSettings());
        }

        /// <summary>
        /// Creates a new http client with custom settings
        /// </summary>
        /// <param name="rootUrl">The root url that all requests will be relative to</param>
        /// <param name="settings">The custom settings to use for this http client</param>
        /// <returns>A new http client</returns>
        public static IHttpClient Create(string rootUrl, HttpClientSettings settings)
        {
            return new HttpClient(rootUrl, settings);
        }

        private readonly IRequestRunner requestRunner;

        private readonly IResourceMerger merger;

        private readonly HttpClientSettings settings;

        internal HttpClient(string rootUrl, HttpClientSettings settings)
        {
            this.settings = settings;

            settings.Validate();

            var cookieContainer = new CookieContainer();
            var client = BuildSystemClient(cookieContainer, settings.DefaultTimeout);

            requestRunner = new RequestRunner(
                client,
                new TransmissionSettings(settings.Serializers),
                settings.QuerySerializer,
                cookieContainer,
                settings.Middleware.Clone());

            merger = new ResourceMerger(settings.NamingConvention);

            Root = Resource.Create(rootUrl);
        }

        internal HttpClient(
            string rootUrl,
            HttpClientSettings settings,
            IRequestRunner requestRunner)
        {
            this.settings = settings;

            settings.Validate();

            this.requestRunner = requestRunner;

            merger = new ResourceMerger(settings.NamingConvention);

            Root = Resource.Create(rootUrl);
        }

        public Resource Root { get; }

        internal System.Net.Http.HttpClient BuildSystemClient(CookieContainer cookieContainer, TimeSpan? defaultTimeout)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseDefaultCredentials = false,
                CookieContainer = cookieContainer,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None
            };

            settings.Authenticator.Authenticate(handler);

            var httpClient = new System.Net.Http.HttpClient(handler);

            if (defaultTimeout != null)
            {
                httpClient.Timeout = defaultTimeout.Value;
            }

            settings.Authenticator.Authenticate(httpClient);

            return httpClient;
        }

        public Task<IHttpResponse> Get(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new GetRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Post(object body, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments ?? body, segments != null);
            var request = new PostRequest(merged, new ObjectRequestBody(body));
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Post(IFile file, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Post(new[] { file }, relativeUrl, segments, cancellationToken);
        }

        public Task<IHttpResponse> Post(IFile[] files, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PostRequest(merged, new FileUploadBody(merged, files));
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Post(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PostRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Put(object body, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments ?? body, segments != null);
            var request = new PutRequest(merged, new ObjectRequestBody(body));
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Put(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PutRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Put(IFile file, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Put(new[] { file }, relativeUrl, segments, cancellationToken);
        }

        public Task<IHttpResponse> Put(IFile[] files, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PutRequest(merged, new FileUploadBody(merged, files));
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Patch(object body, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments ?? body, segments != null);
            var request = new PatchRequest(merged, new ObjectRequestBody(body));
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Patch(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PatchRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Patch(IFile file, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Patch(new[] { file }, relativeUrl, segments, cancellationToken);
        }

        public Task<IHttpResponse> Patch(IFile[] files, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PatchRequest(merged, new FileUploadBody(merged, files));
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Delete(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new DeleteRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Head(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new HeadRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Options(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new OptionsRequest(merged);
            return Run(request, cancellationToken);
        }

        public Task<IHttpResponse> Run<T>(T request, CancellationToken cancellationToken = default(CancellationToken))
            where T : IHttpRequest
        {
            return requestRunner.RunAsync(request, cancellationToken);
        }

        public Resource BuildRelativeResource(string relativeUrl, object segments, bool shouldMergeProperties = true)
        {
            var resource = Root.Append(relativeUrl);
            return merger.Merge(resource, segments, shouldMergeProperties);
        }
    }
}
