using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.IntegrationTests.Middleware;
using SpeakEasy.Middleware;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    public class MiddlewareTests
    {
        [Fact]
        public async void ShouldAddCustomHeaders()
        {
            var settings = new HttpClientSettings();

            settings.AddMiddleware(new ConsoleLoggingMiddleware());
            settings.AddMiddleware(new CustomHeadersMiddleware());

            var client = HttpClient.Create("http://localhost:1337/api", settings);

            var response = await client
                .Get("products/1");

            Assert.Contains(":1337/api/products/1", response.State.RequestUrl.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void ShouldHaveCustomUserAgent()
        {
            var settings = new HttpClientSettings();

            settings.AddMiddleware(new ConsoleLoggingMiddleware());
            settings.ReplaceMiddleware(new UserAgentMiddleware(new UserAgent("MyFancyUserAgent")));

            var client = HttpClient.Create("http://localhost:1337/api", settings);

            var response = await client
                .Get("products/1");

            Assert.Contains(":1337/api/products/1", response.State.RequestUrl.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public class CustomHeadersMiddleware : IHttpMiddleware
        {
            public IHttpMiddleware Next { get; set; }

            public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
            {
                request.AddHeader("x-special-header", "frank");
                request.AddHeader(x => x.ExpectContinue = true);

                return await Next.Invoke(request, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
