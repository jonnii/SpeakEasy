namespace SpeakEasy
{
    public sealed class DeleteRequest : GetLikeRequest
    {
        public DeleteRequest(Resource resource)
            : base(resource)
        {
        }

        public override string HttpMethod { get; } = "DELETE";
    }
}
