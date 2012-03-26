using System;

namespace Resticle.Examples
{
	public class Examples
	{
		public void Run()
		{
			IRestClient client = new RestClient("http://localhost:8080/api");
			
			client.Get("company/:name", new { name = "this is the name of the company" })
				.On(200, () => Console.WriteLine ("this is good to go!"));
		
			var body = new { id = "lol", party_planner_pants = true };
			
			client.Post(body, "company/:id", new { id = "company_name" }).OnOK(() => {
				Console.WriteLine("this goes here");
			});
			
			var company = new Resource<Company>("company/:id");
			
			client.Get(company.Id("abcd"));
		}
				
		public static void Main(string[] args)
		{
			var examples = new Examples();
			examples.Run();
		}
	}
}

