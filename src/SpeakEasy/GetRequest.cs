namespace SpeakEasy
{
    public sealed class GetRequest : GetLikeRequest
    {
        public GetRequest(Resource resource)
            : base(resource)
        {

        }

        public override string ToString()
        {
            return string.Format("[GetRequest {0}]", Resource);
        }
    }
}