using System;
using System.Collections.Generic;
using Resticle.Authenticators;
using Resticle.Samples.Basecamp.Models;

namespace Resticle.Samples.Basecamp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = RestClientSettings.Default;
            settings.Authenticator = new BasicAuthenticator("username", "password");
            settings.UserAgent = "Resticle (username)";

            var client = RestClient.Create("https://basecamp.com/1816438/api/v1/", settings);

            var projects = client.Get("projects.json").OnOk().Unwrap<List<Project>>();

            foreach (var project in projects)
            {
                Console.WriteLine(project.Name);
            }

            var person = client.Get("people/me.json").OnOk().Unwrap<Person>();
            Console.WriteLine(person.Name);
            Console.WriteLine(person.EmailAddress);

            Console.ReadLine();
        }
    }
}
