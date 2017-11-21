using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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

        public HttpClient(string rootUrl, HttpClientSettings settings)
        {
            settings.Validate();

            requestRunner = new RequestRunner(
                new TransmissionSettings(settings.Serializers),
                settings.Authenticator,
                settings.ArrayFormatter,
                new CookieContainer(),
                settings.UserAgent);

            merger = new ResourceMerger(settings.NamingConvention);

            UserAgent = settings.UserAgent;
            Root = new Resource(rootUrl);
            InstrumentationSink = settings.InstrumentationSink;
        }

        public HttpClient(
            IRequestRunner requestRunner,
            INamingConvention namingConvention,
            IInstrumentationSink instrumentationSink,
            IUserAgent userAgent)
        {
            this.requestRunner = requestRunner;

            UserAgent = userAgent;
            InstrumentationSink = instrumentationSink;

            merger = new ResourceMerger(namingConvention);
        }

        public event EventHandler<BeforeRequestEventArgs> BeforeRequest;

        public event EventHandler<AfterRequestEventArgs> AfterRequest;

        public IInstrumentationSink InstrumentationSink { get; }

        public Resource Root { get; set; }

        public IUserAgent UserAgent { get; }

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

        public async Task<IHttpResponse> Run<T>(T request, CancellationToken cancellationToken = default(CancellationToken))
            where T : IHttpRequest
        {
            OnBeforeRequest(request);

            var watch = Stopwatch.StartNew();
            var response = await requestRunner.RunAsync(request, cancellationToken).ConfigureAwait(false);
            watch.Stop();

            OnAfterRequest(request, response, watch.ElapsedMilliseconds);

            return response;
        }

        public IHttpEndpoint Pipeline(Action<IHttpPipeline> action)
        {
            throw new NotImplementedException();
        }

        public Resource BuildRelativeResource(string relativeUrl, object segments, bool shouldMergeProperties = true)
        {
            var resource = Root.Append(relativeUrl);
            return merger.Merge(resource, segments, shouldMergeProperties);
        }

        private void OnBeforeRequest(IHttpRequest request)
        {
            InstrumentationSink.BeforeRequest(request);
            BeforeRequest?.Invoke(this, new BeforeRequestEventArgs(request));
        }

        private void OnAfterRequest(IHttpRequest request, IHttpResponse response, long elapsedMs)
        {
            AfterRequest?.Invoke(this, new AfterRequestEventArgs(request, response, elapsedMs));
            InstrumentationSink.AfterRequest(request, response, elapsedMs);
        }
    }
}
