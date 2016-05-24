namespace SpeakEasy
{
    public sealed class DeleteRequest : GetLikeRequest
    {
        public DeleteRequest(Resource resource)
            : base(resource)
        {
        }

        public override string HttpMethod { get; } = "DELETE";

        public override string ToString()
        {
            return $"[DeleteRequest {Resource}]";
        }
    }
}
