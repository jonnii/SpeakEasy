namespace HttpSpeak
{
    public sealed class PatchRequest : PostLikeRequest
    {
        public PatchRequest(Resource resource)
            : base(resource)
        {

        }

        public PatchRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {

        }

        protected override string GetHttpMethod()
        {
            return "PATCH";
        }
    }
}