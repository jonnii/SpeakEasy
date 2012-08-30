using System;
using System.Collections.Generic;
using SpeakEasy.Authenticators;
using SpeakEasy.Samples.Basecamp.Models;

namespace SpeakEasy.Samples.Basecamp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = new HttpClientSettings
            {
                Authenticator = new BasicAuthenticator("username", "password"),
                UserAgent = new UserAgent("SpeakEasy (username)")
            };

            var client = HttpClient.Create("https://basecamp.com/1816438/api/v1/", settings);

            var projects = client.Get("projects.json").OnOk().As<List<Project>>();

            foreach (var project in projects)
            {
                Console.WriteLine(project.Name);
            }

            var person = client.Get("people/me.json").OnOk().As<Person>();
            Console.WriteLine(person.Name);
            Console.WriteLine(person.EmailAddress);

            Console.ReadLine();
        }
    }
}
