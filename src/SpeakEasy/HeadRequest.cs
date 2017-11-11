namespace SpeakEasy
{
    public sealed class HeadRequest : GetLikeRequest
    {
        public HeadRequest(Resource resource)
            : base(resource)
        {
        }

        public override string HttpMethod { get; } = "HEAD";
    }
}
