using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Instrumentation;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    //[Collection("Api collection")]
    public class MiddlewareTests
    {
        //private readonly IHttpClient client;

        //public MiddlewareTests(ApiFixture fixture)
        //{
        //    client = fixture.Client;
        //}

        [Fact]
        public async void ShouldGetAsync()
        {
            var settings = new HttpClientSettings
            {
                InstrumentationSink = new ConsoleInstrumentationSink(),
            };

            //settings.Middleware.Add(new CustomHeadersMiddleware());

            var client = HttpClient.Create("http://localhost:1337/api", settings);

            var response = await client
                .Get("products/1");

            Assert.Contains(":1337/api/products/1", response.State.RequestUrl.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    public class CustomHeadersMiddleware : IHttpMiddleware
    {
        public IHttpMiddleware Next { get; set; }

        public Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            request.AddHeader("x-special-header", "frank");
            request.AddHeader(x => x.ExpectContinue = true);

            return Next.Invoke(request, cancellationToken);
        }
    }
}
