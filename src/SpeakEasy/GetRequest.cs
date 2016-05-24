namespace SpeakEasy
{
    public sealed class GetRequest : GetLikeRequest
    {
        public GetRequest(Resource resource)
            : base(resource)
        {
        }

        public override string HttpMethod { get; } = "GET";

        public override string ToString()
        {
            return $"[GetRequest {Resource}]";
        }
    }
}
