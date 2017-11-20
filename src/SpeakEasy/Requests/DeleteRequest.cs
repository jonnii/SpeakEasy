using System.Net.Http;

namespace SpeakEasy.Requests
{
    internal sealed class DeleteRequest : GetLikeRequest
    {
        public DeleteRequest(Resource resource)
            : base(resource)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Delete;
    }
}
