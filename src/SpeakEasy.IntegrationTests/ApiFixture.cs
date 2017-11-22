using System;
using Microsoft.AspNetCore.Hosting;
using SpeakEasy.IntegrationTests.Middleware;

namespace SpeakEasy.IntegrationTests
{
    public class ApiFixture : IDisposable
    {
        public static string ApiUrl => "http://*:1337/";

        private readonly IWebHost host;

        public ApiFixture()
        {
            host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls(ApiUrl)
                .Build();

            host.Start();

            var settings = new HttpClientSettings();
            settings.AddMiddleware(new ConsoleLoggingMiddleware());

            Client = HttpClient.Create("http://localhost:1337/api", settings);
        }

        public IHttpClient Client { get; }

        public void Dispose()
        {
            host.Dispose();
        }
    }
}
