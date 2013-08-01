using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SpeakEasy.Authenticators;
using SpeakEasy.Serializers;

namespace SpeakEasy.Samples.RabbitMq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = new HttpClientSettings
            {
                Authenticator = new BasicAuthenticator("guest", "guest")
            };

            const string root = "http://localhost:55672/api/";

            //settings.Configure<SimpleJsonSerializer>(s =>
            //    s.ConfigureSettings(c =>
            //    {
            //        c.ContractResolver = new RabbitContractResolver();
            //    }));

            var client = HttpClient.Create(root, settings);

            client.BeforeRequest += delegate(object sender, BeforeRequestEventArgs eventArgs)
            {
                eventArgs.Request.AddHeader("Accept", string.Empty);
            };

            var resource = client.BuildRelativeResource("vhosts", new { });

            var vhosts = client.Get("vhosts")
               .OnOk()
               .As<List<Vhost>>();

            Console.WriteLine(vhosts[0].Name);

            Console.ReadLine();
        }
    }

    public class Vhost
    {
        public string Name { get; set; }

        public bool Tracing { get; set; }
    }


    //public class RabbitContractResolver : DefaultContractResolver
    //{
    //    protected override string ResolvePropertyName(string propertyName)
    //    {
    //        return Regex.Replace(propertyName, "([a-z])([A-Z])", "$1_$2").ToLower();
    //    }
    //}
}
