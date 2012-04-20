namespace SpeakEasy
{
    public sealed class HeadRequest : GetLikeRequest
    {
        public HeadRequest(Resource resource)
            : base(resource)
        {

        }

        public override string HttpMethod
        {
            get { return "HEAD"; }
        }

        public override string ToString()
        {
            return string.Format("[HeadRequest {0}]", Resource);
        }
    }
}