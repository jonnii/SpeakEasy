using System;
using System.Threading;
using SpeakEasy.Loggers;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SpeakEasy.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class ApiFixture : IDisposable 
    {
        public static string ApiUrl => $"http://{Environment.MachineName}:1337/api";
        
        private Thread hostThread;

        public ApiFixture()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls(ApiUrl)
                .Build();

            hostThread = new Thread(() => host.Run());
            hostThread.Start();

            var settings = new HttpClientSettings
            {
                Logger = new ConsoleLogger()
            };

            Client = HttpClient.Create(ApiUrl, settings);
        }

        public IHttpClient Client { get; }

        public void Dispose()
        {
            hostThread.Stop();
        }
    }

    [CollectionDefinition("Api collection")]
    public class ApiCollection : ICollectionFixture<ApiFixture>
    {
    }


    // public class WithApi
    // {

    //     private static readonly Lazy<HttpSelfHostServer> ApiServer = new Lazy<HttpSelfHostServer>(() =>
    //     {
    //         var config = new HttpSelfHostConfiguration("http://localhost:1337");

    //         config.Routes.MapHttpRoute("api", "api/{controller}/{id}", new { id = RouteParameter.Optional });
    //         config.Routes.MapHttpRoute("reservations_api", "api/products/{productId}/{controller}/{id}", new { id = RouteParameter.Optional });
    //         config.Formatters.XmlFormatter.UseXmlSerializer = true;

    //         var server = new HttpSelfHostServer(config);
    //         server.OpenAsync().Wait();
    //         return server;
    //     });

    // }
}
