using System;

namespace Resticle
{
    public class RestRequestBuilder : IRestRequestBuilder
    {
        private readonly GetRestRequest request;

        public RestRequestBuilder(Resource root, string path, object segments = null)
        {
            var mergedUri = root.Append(path).Merge(segments);
            var uri = new Uri(mergedUri);

            request = new GetRestRequest(uri);
        }

        public IRestRequest Build()
        {
            return request;
        }
    }
}