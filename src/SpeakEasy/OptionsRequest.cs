namespace SpeakEasy
{
    internal sealed class OptionsRequest : GetLikeRequest
    {
        public OptionsRequest(Resource resource)
            : base(resource)
        {

        }

        public override string HttpMethod { get; } = "OPTIONS";

        public override string ToString()
        {
            return $"[OptionsRequest {Resource}]";
        }
    }
}
