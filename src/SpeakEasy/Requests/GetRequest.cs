using System.Net.Http;

namespace SpeakEasy.Requests
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
