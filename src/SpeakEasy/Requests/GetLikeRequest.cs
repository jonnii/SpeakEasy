using SpeakEasy.Bodies;

namespace SpeakEasy.Requests
{
    internal abstract class GetLikeRequest : HttpRequest
    {
        protected GetLikeRequest(Resource resource)
            : base(resource, new NullRequestBody())
        {
        }
    }
}
