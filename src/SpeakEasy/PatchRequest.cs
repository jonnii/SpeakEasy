namespace SpeakEasy
{
    internal sealed class PatchRequest : PostLikeRequest
    {
        public PatchRequest(Resource resource)
            : base(resource)
        {
        }

        public PatchRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }

        public override string HttpMethod { get; } = "PATCH";

        public override string ToString()
        {
            return $"[PatchRequest {Resource}]";
        }
    }
}
