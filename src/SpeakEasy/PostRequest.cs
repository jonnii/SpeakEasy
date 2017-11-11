namespace SpeakEasy
{
    public sealed class PostRequest : PostLikeRequest
    {
        public PostRequest(Resource resource)
            : base(resource)
        {
        }

        public PostRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }

        public override string HttpMethod { get; } = "POST";
    }
}
