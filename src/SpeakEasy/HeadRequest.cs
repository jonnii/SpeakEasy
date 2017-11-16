using System.Net.Http;
using SpeakEasy.Requests;

namespace SpeakEasy
{
    public sealed class HeadRequest : GetLikeRequest
    {
        public HeadRequest(Resource resource)
            : base(resource)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Head;
    }
}
