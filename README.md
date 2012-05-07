SpeakEasy [![endorse](http://api.coderwall.com/jonnii/endorsecount.png)](http://coderwall.com/jonnii)
=========

SpeakEasy is a library for working with web apis in the language they speak best, http. It's heavily 
inspired by jQuery and RestSharp, so if you've used either of those libraries you should feel right
at home.

Show me the goods
-----------------

    // create a client
    var client = HttpClient.Create("http://example.com/api");
    
    // get some companies!
    var companies = client.Get("companies").OnOk().As<List<Company>>();
  
    // upload a company, with validation error support
    client.Post(company, "companies")
        .On(HttpStatusCode.BadRequest, (List<ValidationError> errors) => {
          Console.WriteLine("Ruh Roh, you have {0} validation errors", errors.Count());
        })
        .On(HttpStatusCode.Created, () => Console.WriteLine("Holy moly you win!"));
    
    // update a company
    client.Put(company, "company/:id", new { id = "awesome-sauce" })
        .OnOk(() => Console.WriteLine("Company updated"));
        
    // run a search
    client.Get("images/:category", new { category = "cats", breed = "omg the cutest", size = "kittens" })
        .OnOk().As<List<Image>>();
    
    // Asynchronous
    client.GetAsync("companies/:id", new { id = 5 }).
        .OnComplete(r => r.OnOk(UpdateCompaniesCallback))
        .Start();


How do I get it?
----------------

If you aren't using nuget, then shame on you. If you are, then installation is as simple as:

    # install the stable version
    install-package SpeakEasy
	
    # install the pre-release version
    install-package SpeakEasy -pre

SpeakEasy has a dependency on Newtonsoft.Json.

Builds
------

Builds are managed by code better. (http://teamcity.codebetter.com/login.html)

Contributing
------------

I'd love if you could contribute. You could:

* Add serializers.
* Add authentication providers (oauth?)
* Write examples and documentation!

However, if you're going to contribute please make sure you write tests for any new functionality.
Adding an example is also great!