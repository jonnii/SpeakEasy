namespace SpeakEasy.Specifications
{
    public class TestHttpRequest : GetLikeRequest
    {
        public TestHttpRequest(Resource resource)
            : base(resource) { }

        public override string ToString()
        {
            return "[TestRequest]";
        }
    }
}
