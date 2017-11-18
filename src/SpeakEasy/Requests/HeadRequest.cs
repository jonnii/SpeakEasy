using System.Net.Http;

namespace SpeakEasy.Requests
{
    internal sealed class HeadRequest : GetLikeRequest
    {
        public HeadRequest(Resource resource)
            : base(resource)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Head;
    }
}
