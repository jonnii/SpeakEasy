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

        public override string HttpMethod
        {
            get { return "PATCH"; }
        }

        public override string ToString()
        {
            return string.Format("[PatchRequest {0}]", Resource);
        }
    }
}