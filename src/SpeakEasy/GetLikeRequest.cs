namespace SpeakEasy
{
    public abstract class GetLikeRequest : HttpRequest
    {
        protected GetLikeRequest(Resource resource)
            : base(resource, new NullRequestBody())
        {

        }
    }
}