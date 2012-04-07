using System;

namespace Resticle
{
    public class RestClient : IRestClient
    {
        public static IRestClient Create(string url)
        {
            return Create(url, RestClientSettings.Default);
        }

        public static IRestClient Create(string url, RestClientSettings settings)
        {
            var transmision = new Transmission(settings.DefaultSerializer, settings.Deserializers);

            var runner = new RequestRunner(transmision, new WebRequestGateway());

            return new RestClient(runner)
            {
                Root = new Resource(url)
            };
        }

        private readonly IRequestRunner requestRunner;

        public RestClient(IRequestRunner requestRunner)
        {
            this.requestRunner = requestRunner;

            DefaultSerializer = Serializer.Json;
        }

        public Resource Root { get; set; }

        public Func<ISerializer> DefaultSerializer { get; set; }

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

        public IRestResponse Post(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body);
            var request = new PostRestRequest(resource, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Post(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PostRestRequest(resource);
            return requestRunner.Run(request);
        }

        public IRestResponse Put(object body, string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments ?? body);
            var request = new PutRestRequest(resource, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Put(string relativeUrl, object segments = null)
        {
            var resource = Root.Append(relativeUrl).Merge(segments);
            var request = new PutRestRequest(resource);
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
    }
}