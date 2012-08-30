using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
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

            settings.Configure<JsonDotNetSerializer>(s =>
                s.ConfigureSettings(c =>
                {
                    c.ContractResolver = new RabbitContractResolver();
                }));

            var client = HttpClient.Create(root, settings);

            var options = new
            {
                vhost = "integration",
                name = "ErrorQueue",
                count = "1",
                requeue = "true",
                encoding = "auto",
                truncate = "50000"
            };

            var messages = client.Post(options, "queues/:vhost/:queue/get", new { vhost = "integration", queue = "ErrorQueue" })
                .OnOk()
                .As<List<Message>>();

            Console.WriteLine(messages.Count);

            Console.ReadLine();
        }
    }

    public class Message { }

    public class RabbitContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return Regex.Replace(propertyName, "([a-z])([A-Z])", "$1_$2").ToLower();
        }
    }
}
