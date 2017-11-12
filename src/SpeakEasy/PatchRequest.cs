using System.Net.Http;

namespace SpeakEasy
{
    internal sealed class PatchRequest : PostLikeRequest
    {
        private static readonly HttpMethod HttpMethodPatch = new HttpMethod("PATCH");

        public PatchRequest(Resource resource)
            : base(resource)
        {
        }

        public PatchRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethodPatch;
    }
}
