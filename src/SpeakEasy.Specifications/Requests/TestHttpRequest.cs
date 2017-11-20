using System.Net.Http;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    internal class TestHttpRequest : GetLikeRequest
    {
        public TestHttpRequest(Resource resource)
            : base(resource) { }

        public override HttpMethod HttpMethod => new HttpMethod("TEST");
    }
}
