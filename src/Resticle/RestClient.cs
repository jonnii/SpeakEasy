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

        public IRestResponse Get(string relativeUrl, object segments = null)
        {
            var url = Root.Append(relativeUrl).Merge(segments);
            var request = new GetRestRequest(url);
            return requestRunner.Run(request);
        }

        public IRestResponse Post(object body, string relativeUrl, object segments = null)
        {
            var url = Root.Append(relativeUrl).Merge(segments ?? body);
            var request = new PostRestRequest(url, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Put(object body, string relativeUrl, object segments = null)
        {
            var url = Root.Append(relativeUrl).Merge(segments ?? body);
            var request = new PutRestRequest(url, body);
            return requestRunner.Run(request);
        }

        public IRestResponse Delete(string relativeUrl, object segments = null)
        {
            var url = Root.Append(relativeUrl).Merge(segments);
            var request = new DeleteRestRequest(url);
            return requestRunner.Run(request);
        }
    }
}