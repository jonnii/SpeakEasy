using System.Net.Http;

namespace SpeakEasy.Requests
{
    internal sealed class PostRequest : PostLikeRequest
    {
        public PostRequest(Resource resource)
            : base(resource)
        {
        }

        public PostRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }

        public override HttpMethod HttpMethod { get; } = HttpMethod.Post;
    }
}
