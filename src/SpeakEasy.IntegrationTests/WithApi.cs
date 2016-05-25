using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using NUnit.Framework;
using SpeakEasy.Loggers;

namespace SpeakEasy.IntegrationTests
{
    public class WithApi
    {
        public static string ApiUrl => $"http://{Environment.MachineName}:1337/api";

        private static readonly Lazy<HttpSelfHostServer> ApiServer = new Lazy<HttpSelfHostServer>(() =>
        {
            var config = new HttpSelfHostConfiguration("http://localhost:1337");

            config.Routes.MapHttpRoute("api", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("reservations_api", "api/products/{productId}/{controller}/{id}", new { id = RouteParameter.Optional });
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            return server;
        });

        private static HttpSelfHostServer StartWebServer()
        {
            return ApiServer.Value;
        }

        protected IHttpClient client;

        protected HttpSelfHostServer server;

        protected TrackingStreamManager trackingStreamManager;

        [OneTimeSetUp]
        public void StartServer()
        {
            trackingStreamManager = new TrackingStreamManager();

            server = StartWebServer();
            client = CreateClient();
        }

        [SetUp]
        public void Setup()
        {
            trackingStreamManager.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            trackingStreamManager.CheckForUnDisposedStreams();
        }

        protected virtual IHttpClient CreateClient()
        {
            var settings = new HttpClientSettings
            {
                Logger = new ConsoleLogger(),
                StreamManager = trackingStreamManager
            };

            return HttpClient.Create(ApiUrl, settings);
        }
    }
}
