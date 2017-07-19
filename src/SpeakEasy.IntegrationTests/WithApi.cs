using System;
using System.Threading;
using SpeakEasy.Loggers;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SpeakEasy.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SpeakEasy", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }

    public class ApiFixture : IDisposable 
    {
        public static string ApiUrl => $"http://*:1337/";
        
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
            //hostThread.Stop();
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
    //     });
    // }
}
