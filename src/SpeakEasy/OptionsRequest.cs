namespace SpeakEasy
{
    internal sealed class OptionsRequest : GetLikeRequest
    {
        public OptionsRequest(Resource resource)
            : base(resource)
        {

        }

        public override string HttpMethod
        {
            get { return "OPTIONS"; }
        }

        public override string ToString()
        {
            return string.Format("[OptionsRequest {0}]", Resource);
        }
    }
}