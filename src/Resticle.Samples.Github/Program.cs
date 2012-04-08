using System;
using System.Collections.Generic;
using Resticle.Serializers;

namespace Resticle.Samples.Github
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = RestClientSettings.Default;
            settings.Configure<JsonDotNetSerializer>(
                j => j.ConfigureSettings(s => s.ContractResolver = new GithubContractResolver()));

            var client = RestClient.Create("http://github.com/api/v2/json", settings);

            var repositories = client.Get("repos/show/:user", new { user = "jonnii" }).OnOk()
                .Unwrap<List<Repository>>(new DeserializationSettings { RootElementPath = "repositories" });

            foreach (var repository in repositories)
            {
                Console.WriteLine(repository.Name);
                Console.WriteLine(repository.Description);
                Console.WriteLine(repository.Url);
                Console.WriteLine(repository.CreatedAt);
                Console.WriteLine(repository.PushedAt);
            }

            Console.ReadLine();
        }
    }
}
