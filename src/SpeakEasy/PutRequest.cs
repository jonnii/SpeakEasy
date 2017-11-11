namespace SpeakEasy
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

        public override string HttpMethod { get; } = "PUT";
    }
}
