using System.Linq;

namespace Resticle
{
    public abstract class GetLikeRestRequest : RestRequest
    {
        protected GetLikeRestRequest(Resource resource)
            : base(resource) { }

        public override string BuildRequestUrl(Resource resource)
        {
            if (!resource.HasParameters)
            {
                return resource.Path;
            }

            var queryString = string.Join("&", resource.Parameters.Select(p => p.ToQueryString()));
            return string.Concat(resource.Path, "?", queryString);
        }
    }
}