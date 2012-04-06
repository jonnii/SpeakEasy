using System;

namespace Resticle
{
    public class RestRequestBuilder : IRestRequestBuilder
    {
        private readonly Resource root;

        private readonly string path;

        private readonly object segments;

        public RestRequestBuilder(Resource root, string path, object segments = null)
        {
            this.root = root;
            this.path = path;
            this.segments = segments;
        }

        public TRequest Build<TRequest>(Func<Uri, TRequest> builder)
            where TRequest : IRestRequest
        {
            var mergedUri = root.Append(path).Merge(segments);
            var url = new Uri(mergedUri);

            return builder(url);
        }
    }
}