namespace SpeakEasy
{
    public abstract class PostLikeRequest : HttpRequest
    {
        protected PostLikeRequest(Resource resource)
            : base(resource, new PostRequestBody(resource))
        {
        }

        protected PostLikeRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }

        public override string BuildRequestUrl()
        {
            return Resource.Path;
        }
    }
}