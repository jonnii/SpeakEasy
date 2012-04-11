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

        protected override string GetHttpMethod()
        {
            return "PUT";
        }
    }
}