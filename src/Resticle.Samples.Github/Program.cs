using System;
using System.Collections.Generic;
using Resticle.Samples.Github.Models;
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

            var commits = client.Get("commits/list/:user/:repository/:branch", new { user = "jonnii", repository = "resticle", branch = "master" })
                .OnOk().Unwrap<List<Commit>>(new DeserializationSettings { RootElementPath = "commits" });

            foreach (var commit in commits)
            {
                Console.WriteLine(commit.Message);
            }

            Console.ReadLine();
        }
    }
}
