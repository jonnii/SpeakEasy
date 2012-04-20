namespace SpeakEasy.Specifications
{
    public class TestHttpRequest : GetLikeRequest
    {
        public TestHttpRequest(Resource resource)
            : base(resource) { }

        public override string HttpMethod
        {
            get { return "TEST"; }
        }

        public override string ToString()
        {
            return "[TestRequest]";
        }
    }
}
