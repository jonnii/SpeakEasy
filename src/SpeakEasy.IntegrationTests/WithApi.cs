using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using NUnit.Framework;
using SpeakEasy.Loggers;

namespace SpeakEasy.IntegrationTests
{
    public class WithApi
    {
        public static string ApiUrl = "http://localhost:1337/api";

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

        [TestFixtureSetUp]
        public void StartServer()
        {
            server = StartWebServer();
            client = CreateClient();
        }

        protected virtual IHttpClient CreateClient()
        {
            var settings = HttpClientSettings.Default;
            settings.Logger = new ConsoleLogger();

            return HttpClient.Create(ApiUrl, settings);
        }
    }
}
