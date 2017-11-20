namespace SpeakEasy.Requests
{
    internal abstract class PostLikeRequest : HttpRequest
    {
        protected PostLikeRequest(Resource resource)
            : base(resource, new PostRequestBody(resource))
        {
        }

        protected PostLikeRequest(Resource resource, IRequestBody body)
            : base(resource, body)
        {
        }
    }
}
