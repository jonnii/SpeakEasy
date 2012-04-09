namespace HttpSpeak
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
            return Create(rootUrl, HttpClientSettings.Default);
        }

        /// <summary>
        /// Creates a new http client with custom settings
        /// </summary>
        /// <param name="rootUrl">The root url that all requests will be relative to</param>
        /// <param name="settings">The custom settings to use for this http client</param>
        /// <returns>A new http client</returns>
        public static IHttpClient Create(string rootUrl, HttpClientSettings settings)
        {
            var transmissionSettings = new TransmissionSettings(settings.Serializers);
            var webRequestGateway = new WebRequestGateway();

            var runner = new RequestRunner(
                transmissionSettings,
                webRequestGateway,
                settings.Authenticator);

            return new HttpClient(runner)
            {
                Root = new Resource(rootUrl),
                Settings = settings
            };
        }

        private readonly IRequestRunner requestRunner;

        public HttpClient(IRequestRunner requestRunner)
        {
            this.requestRunner = requestRunner;

            Settings = new HttpClientSettings();
        }

        public Resource Root { get; set; }

        public HttpClientSettings Settings { get; set; }

        public IHttpResponse Get(Resource resource)
        {
            var appended = Root.Append(resource);
            var request = new GetRequest(appended);
            return Run(request);
        }

        public IHttpResponse Get(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new GetRequest(resource);
            return Run(request);
        }

        public IHttpResponse Post(object body, Resource resource)
        {
            var appended = Root.Append(resource);
            var request = new PostRequest(appended, body);
            return Run(request);
        }

        public IHttpResponse Post(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body, false);
            var request = new PostRequest(resource, body);
            return Run(request);
        }

        public IHttpResponse Post(FileUpload file, string relativeUrl, object segments = null)
        {
            return Post(new[] { file }, relativeUrl, segments);
        }

        public IHttpResponse Post(FileUpload[] files, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments, false);
            var request = new PostRequest(resource, files);
            return Run(request);
        }

        public IHttpResponse Post(Resource resource)
        {
            return Post(resource, null);
        }

        public IHttpResponse Post(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PostRequest(resource);
            return Run(request);
        }

        public IHttpResponse Put(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body, false);
            var request = new PutRequest(resource, body);
            return Run(request);
        }

        public IHttpResponse Put(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PutRequest(resource);
            return Run(request);
        }

        public IHttpResponse Put(FileUpload file, string relativeUrl, object segments = null)
        {
            return Put(new[] { file }, relativeUrl, segments);
        }

        public IHttpResponse Put(FileUpload[] files, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments, false);
            var request = new PutRequest(resource, files);
            return Run(request);
        }

        public IHttpResponse Patch(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body, false);
            var request = new PatchRequest(resource, body);
            return Run(request);
        }

        public IHttpResponse Patch(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PatchRequest(resource);
            return Run(request);
        }

        public IHttpResponse Patch(FileUpload file, string relativeUrl, object segments = null)
        {
            return Patch(new[] { file }, relativeUrl, segments);
        }

        public IHttpResponse Patch(FileUpload[] files, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments, false);
            var request = new PatchRequest(resource, files);
            return Run(request);
        }

        public IHttpResponse Delete(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new DeleteRequest(resource);
            return Run(request);
        }

        public IHttpResponse Head(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new HeadRequest(resource);
            return Run(request);
        }

        public IHttpResponse Options(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new OptionsRequest(resource);
            return Run(request);
        }

        private IHttpResponse Run<T>(T request)
            where T : IHttpRequest
        {
            request.UserAgent = Settings.UserAgent;

            return requestRunner.Run(request);
        }
    }
}