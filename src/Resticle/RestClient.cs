namespace Resticle
{
    public class RestClient : IRestClient
    {
        /// <summary>
        /// Creates a new rest client with default settings
        /// </summary>
        /// <param name="rootUrl">The root url that all requests will be relative to</param>
        /// <returns>A new rest client</returns>
        public static IRestClient Create(string rootUrl)
        {
            return Create(rootUrl, RestClientSettings.Default);
        }

        /// <summary>
        /// Creates a new rest client with custom settings
        /// </summary>
        /// <param name="rootUrl">The root url that all requests will be relative to</param>
        /// <param name="settings">The custom settings to use for this rest client</param>
        /// <returns>A new rest client</returns>
        public static IRestClient Create(string rootUrl, RestClientSettings settings)
        {
            var transmissionSettings = new TransmissionSettings(
                settings.Serializers);

            var runner = new RequestRunner(transmissionSettings, new WebRequestGateway());

            return new RestClient(runner)
            {
                Root = new Resource(rootUrl)
            };
        }

        private readonly IRequestRunner requestRunner;

        public RestClient(IRequestRunner requestRunner)
        {
            this.requestRunner = requestRunner;
        }

        public Resource Root { get; set; }

        public IRestResponse Get(Resource resource)
        {
            var appended = Root.Append(resource);
            var request = new GetRestRequest(appended);
            return requestRunner.Run(request);
        }

        public IRestResponse Get(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new GetRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Post(object body, Resource resource)
        {
            var appended = Root.Append(resource);
            var request = new PostRestRequest(appended, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Post(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body, false);
            var request = new PostRestRequest(resource, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Post(FileUpload file, string relativeUrl, object segments = null)
        {
            return Post(new[] { file }, relativeUrl, segments);
        }

        public IRestResponse Post(FileUpload[] files, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments, false);
            var request = new PostRestRequest(resource, files);
            return requestRunner.Run(request);
        }

        public IRestResponse Post(Resource resource)
        {
            return Post(resource, null);
        }

        public IRestResponse Post(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PostRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Put(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body, false);
            var request = new PutRestRequest(resource, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Put(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PutRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Put(FileUpload file, string relativeUrl, object segments = null)
        {
            return Put(new[] { file }, relativeUrl, segments);
        }

        public IRestResponse Put(FileUpload[] files, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments, false);
            var request = new PutRestRequest(resource, files);
            return requestRunner.Run(request);
        }

        public IRestResponse Patch(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body, false);
            var request = new PatchRestRequest(resource, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Patch(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PatchRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Patch(FileUpload file, string relativeUrl, object segments = null)
        {
            return Patch(new[] { file }, relativeUrl, segments);
        }

        public IRestResponse Patch(FileUpload[] files, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments, false);
            var request = new PatchRestRequest(resource, files);
            return requestRunner.Run(request);
        }

        public IRestResponse Delete(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new DeleteRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Head(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new HeadRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Options(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new OptionsRestRequest(resource);
            return requestRunner.Run(request);
        }
    }
}