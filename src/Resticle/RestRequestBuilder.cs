using System;

namespace Resticle
{
    public class RestRequestBuilder : IRestRequestBuilder
    {
        private RestRequest request;

        public RestRequestBuilder(Resource root, string path, object segments = null)
        {
            var mergedUri = root.Append(path).Merge(segments);
            var uri = new Uri(mergedUri);

            request = new RestRequest(uri);
        }

        public IRestRequest Build()
        {
            return request;
        }

        public RestRequestBuilder WithBody(object body)
        {
            request.Body = body;

            return this;
        }
    }
}