using System.Net.Http;

namespace SpeakEasy
{
    public sealed class GetRequest : GetLikeRequest
    {
        public GetRequest(Resource resource)
            : base(resource)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Get;
    }
}
