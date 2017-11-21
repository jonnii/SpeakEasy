using System.Net;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [Collection("Api collection")]
    public class Headers
    {
        private readonly IHttpClient client;

        public Headers(ApiFixture fixture)
        {
            client = fixture.Client;
        }

        [Fact]
        public async void ShouldGetAsync()
        {
            var response = await client
                .Pipeline(x =>
                {
                    x.BeforeRequest(new Header("x-foo", "bob"));
                    x.BeforeRequest(h => h.CacheControl.MustRevalidate = true);
                })
                .Get("products/1");

            Assert.Contains(":1337/api/products/1", response.State.RequestUrl.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
