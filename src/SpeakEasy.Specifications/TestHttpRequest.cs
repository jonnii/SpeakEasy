namespace SpeakEasy.Specifications
{
    internal class TestHttpRequest : GetLikeRequest
    {
        public TestHttpRequest(Resource resource)
            : base(resource) { }

        public override string HttpMethod => "TEST";
    }
}
