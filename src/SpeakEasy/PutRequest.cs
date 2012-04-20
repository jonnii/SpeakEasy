namespace SpeakEasy
{
    public sealed class PutRequest : PostLikeRequest
    {
        public PutRequest(Resource resource)
            : base(resource)
        {

        }

        public PutRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {

        }

        public override string HttpMethod
        {
            get { return "PUT"; }
        }

        public override string ToString()
        {
            return string.Format("[PutRequest {0}]", Resource);
        }
    }
}