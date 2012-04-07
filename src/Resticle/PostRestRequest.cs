namespace Resticle
{
    public sealed class PostRestRequest : PostLikeRestRequest
    {
        public PostRestRequest(Resource resource)
            : base(resource)
        {

        }

        public PostRestRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        protected override string GetHttpMethod()
        {
            return "POST";
        }
    }
}