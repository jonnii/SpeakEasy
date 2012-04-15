using System;
using System.Collections.Generic;
using SpeakEasy.Samples.Github.Models;
using SpeakEasy.Serializers;

namespace SpeakEasy.Samples.Github
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = HttpClientSettings.Default;
            settings.Configure<JsonDotNetSerializer>(j =>
            {
                j.ConfigureSettings(s => s.ContractResolver = new GithubContractResolver());
                j.DefaultDeserializationSettings = new DeserializationSettings { SkipRootElement = true };
            });

            var client = HttpClient.Create(" https://api.github.com", settings);

            var repositories = client.Get("/users/:user/repos", new { user = "jonnii" }).OnOk()
                .As<List<Repository>>();

            foreach (var repository in repositories)
            {
                Console.WriteLine(repository.Name);
                Console.WriteLine(repository.Description);
                Console.WriteLine(repository.Url);
                Console.WriteLine(repository.CreatedAt);
                Console.WriteLine(repository.PushedAt);
            }

            var commits = client.Get("commits/list/:user/:repository/:branch", new { user = "jonnii", repository = "SpeakEasy", branch = "master" })
                .OnOk().As<List<Commit>>();

            foreach (var commit in commits)
            {
                Console.WriteLine(commit.Message);
            }

            Console.ReadLine();
        }
    }
}
