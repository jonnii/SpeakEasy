using System.Net.Http;

namespace SpeakEasy.Requests
{
    internal sealed class PutRequest : PostLikeRequest
    {
        public PutRequest(Resource resource)
            : base(resource)
        {
        }

        public PutRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Put;
    }
}
