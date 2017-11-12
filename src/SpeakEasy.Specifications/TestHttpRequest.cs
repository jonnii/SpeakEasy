using System.Net.Http;

namespace SpeakEasy.Specifications
{
    internal class TestHttpRequest : GetLikeRequest
    {
        public TestHttpRequest(Resource resource)
            : base(resource) { }

        public override HttpMethod HttpMethod => new HttpMethod("TEST");
    }
}
