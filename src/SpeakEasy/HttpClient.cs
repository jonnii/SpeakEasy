using System;
using System.Threading.Tasks;
using SpeakEasy.Extensions;

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

        public HttpClient(string rootUrl, HttpClientSettings settings)
        {
            settings.Validate();

            requestRunner = new RequestRunner(
                new TransmissionSettings(settings.Serializers),
                settings.Authenticator,
                settings.CookieStrategy,
                settings.ArrayFormatter);

            merger = new ResourceMerger(settings.NamingConvention);

            UserAgent = settings.UserAgent;
            Root = new Resource(rootUrl);
            Logger = settings.Logger;
        }

        public HttpClient(
            IRequestRunner requestRunner,
            INamingConvention namingConvention,
            ISpeakEasyLogger logger,
            IUserAgent userAgent)
        {
            this.requestRunner = requestRunner;

            UserAgent = userAgent;
            Logger = logger;

            merger = new ResourceMerger(namingConvention);
        }

        public event EventHandler<BeforeRequestEventArgs> BeforeRequest;

        public event EventHandler<AfterRequestEventArgs> AfterRequest;

        public ISpeakEasyLogger Logger { get; }

        public Resource Root { get; set; }

        public IUserAgent UserAgent { get; }

        public Task<IHttpResponse> Get(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new GetRequest(merged);
            return RunAsync(request);
        }

        public Task<IHttpResponse> Post(object body, string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments ?? body, segments != null);
            var request = new PostRequest(merged, new ObjectRequestBody(body));
            return RunAsync(request);
        }

        public Task<IHttpResponse> Post(IFile file, string relativeUrl, object segments = null)
        {
            return Post(new[] { file }, relativeUrl, segments);
        }

        public Task<IHttpResponse> Post(IFile[] files, string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PostRequest(merged, new FileUploadBody(merged, files));
            return RunAsync(request);
        }

        public Task<IHttpResponse> Post(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PostRequest(merged);
            return RunAsync(request);
        }

        public Task<IHttpResponse> Put(object body, string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments ?? body, segments != null);
            var request = new PutRequest(merged, new ObjectRequestBody(body));
            return RunAsync(request);
        }

        public Task<IHttpResponse> Put(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PutRequest(merged);
            return RunAsync(request);
        }

        public Task<IHttpResponse> Put(IFile file, string relativeUrl, object segments = null)
        {
            return Put(new[] { file }, relativeUrl, segments);
        }

        public Task<IHttpResponse> Put(IFile[] files, string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PutRequest(merged, new FileUploadBody(merged, files));
            return RunAsync(request);
        }

        public Task<IHttpResponse> Patch(object body, string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments ?? body, segments != null);
            var request = new PatchRequest(merged, new ObjectRequestBody(body));
            return RunAsync(request);
        }

        public Task<IHttpResponse> Patch(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PatchRequest(merged);
            return RunAsync(request);
        }

        public Task<IHttpResponse> Patch(IFile file, string relativeUrl, object segments = null)
        {
            return Patch(new[] { file }, relativeUrl, segments);
        }

        public Task<IHttpResponse> Patch(IFile[] files, string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new PatchRequest(merged, new FileUploadBody(merged, files));
            return RunAsync(request);
        }

        public Task<IHttpResponse> Delete(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new DeleteRequest(merged);
            return RunAsync(request);
        }

        public Task<IHttpResponse> Head(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new HeadRequest(merged);
            return RunAsync(request);
        }

        public Task<IHttpResponse> Options(string relativeUrl, object segments = null)
        {
            var merged = BuildRelativeResource(relativeUrl, segments);
            var request = new OptionsRequest(merged);
            return RunAsync(request);
        }

        public async Task<IHttpResponse> RunAsync<T>(T request)
            where T : IHttpRequest
        {
            request.UserAgent = UserAgent;

            OnBeforeRequest(request);

            var response = await requestRunner.RunAsync(request).ConfigureAwait(false);

            OnAfterRequest(request, response);

            return response;
        }

        public Resource BuildRelativeResource(string relativeUrl, object segments, bool shouldMergeProperties = true)
        {
            var resource = Root.Append(relativeUrl);
            return merger.Merge(resource, segments, shouldMergeProperties);
        }

        private void OnBeforeRequest(IHttpRequest request)
        {
            Logger.BeforeRequest(request);
            BeforeRequest.Raise(this, new BeforeRequestEventArgs(request));
        }

        private void OnAfterRequest(IHttpRequest request, IHttpResponse response)
        {
            AfterRequest.Raise(this, new AfterRequestEventArgs(request, response));
            Logger.AfterRequest(request, response);
        }
    }
}
