using System;
using Microsoft.AspNetCore.Hosting;
using SpeakEasy.Loggers;

namespace SpeakEasy.IntegrationTests
{
    public class ApiFixture : IDisposable
    {
        public static string ApiUrl => "http://*:1337/";

        private IWebHost host;

        public ApiFixture()
        {
            host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls(ApiUrl)
                .Build();

            host.Start();

            var settings = new HttpClientSettings
            {
                Logger = new ConsoleLogger()
            };

            Client = HttpClient.Create("http://localhost:1337/api", settings);
        }

        public IHttpClient Client { get; }

        public void Dispose()
        {
            host.Dispose();
        }
    }
}
