using System.Net.Http;

namespace SpeakEasy.Requests
{
    internal sealed class OptionsRequest : GetLikeRequest
    {
        public OptionsRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Options;
    }
}
