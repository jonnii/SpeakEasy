namespace Resticle
{
    public sealed class PutRestRequest : PostLikeRestRequest
    {
        public PutRestRequest(Resource resource)
            : base(resource)
        {

        }

        public PutRestRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        protected override string GetHttpMethod()
        {
            return "PUT";
        }
    }
}