using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Authenticators.Jwt;
using SpeakEasy.IntegrationTests.Middleware;
using SpeakEasy.Middleware;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [Collection("Api collection")]
    public class MiddlewareTests
    {
        private readonly ApiFixture fixture;

        public MiddlewareTests(ApiFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("bob")]
        [InlineData("jeff")]
        public async Task ShouldAddCustomHeaders(string name)
        {
            var settings = new HttpClientSettings();

            settings.Middleware.Append(new ConsoleLoggingMiddleware());
            settings.Middleware.Append(new CustomHeadersMiddleware(name));

            var client = HttpClient.Create("http://localhost:1337/api", settings);

            var response = await client
                .Get("middleware/echo-header", new { headerName = "x-special-name" })
                .OnOk()
                .AsString();

            Assert.Contains(name, response);
        }

        [Theory]
        [InlineData("FancyAgent")]
        [InlineData("MozillaForReal")]
        public async Task ShouldHaveCustomUserAgent(string userAgent)
        {
            var settings = new HttpClientSettings();

            settings.Middleware.Append(new ConsoleLoggingMiddleware());
            settings.Middleware.Replace(new UserAgentMiddleware(new UserAgent(userAgent)));

            var client = HttpClient.Create("http://localhost:1337/api", settings);

            var response = await client
                .Get("middleware/echo-user-agent")
                .OnOk()
                .AsString();

            Assert.Equal(userAgent, response);
        }

        [Fact]
        public async Task ShouldAddJwtHeader()
        {
            var settings = new HttpClientSettings();
            settings.Middleware.Append(new ConsoleLoggingMiddleware());
            settings.AddJwtMiddleware(new HttpJsonJwtStrategy("http://localhost:1337/api/token"));

            var client = HttpClient.Create("http://localhost:1337/api", settings);

            var response = await client
                .Get("middleware/authorization")
                .OnOk()
                .AsString();

            var token = response.Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

            Assert.StartsWith("Bearer ", response);
            Assert.Null(Record.Exception(() => new JwtSecurityToken(token)));
        }

        public class CustomHeadersMiddleware : IHttpMiddleware
        {
            private readonly string name;

            public CustomHeadersMiddleware(string name)
            {
                this.name = name;
            }

            public IHttpMiddleware Next { get; set; }

            public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
            {
                request.AddHeader("x-special-name", name);
                request.AddHeader(x => x.ExpectContinue = true);

                return await Next.Invoke(request, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
