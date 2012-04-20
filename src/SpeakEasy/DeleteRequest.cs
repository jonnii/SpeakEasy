namespace SpeakEasy
{
    public sealed class DeleteRequest : GetLikeRequest
    {
        public DeleteRequest(Resource resource)
            : base(resource)
        {

        }

        public override string HttpMethod
        {
            get { return "DELETE"; }
        }

        public override string ToString()
        {
            return string.Format("[DeleteRequest {0}]", Resource);
        }
    }
}