SpeakEasy
=========

**PLEASE NOTE THIS IS A WORK IN PROGRESS**
**IF YOU HAVE FEEDBACK PLEASE EMAIL ME**
**me@[mygithubusername].com**

This is a super simple library to talk to http web services, the kind you can easily create using
ASP.net MVC WebApi or [insert your favourite web framework here]. It's heavily inspired 
by [RestSharp](http://restsharp.org) and therefore licensed the same. It exists primarily to scratch 
an itch I had that RestSharp couldn't and if you find it useful then great!

I highly recommend you **DO NOT USE THIS** library (yet). It most likely will not do anything you want. You are 
far better served by using RestSharp. It supports async, it has xml support, it has a larger community,
John Sheehan is far smarter than I am, it has documentation, it works. etc... 

Contributing
------------

I'd love if you could contribute. You could:

* Add serializers (for example, xml)
* Add authentication providers (oauth?)
* Write examples and documentation! (everyone needs this)

However, if you're going to contribute please make sure you write tests for any new functionality.
Adding an example is also great!

Installation
------------

If you aren't using nuget, then shame on you. If you are, then installation is as simple as:

    install-package SpeakEasy

SpeakEasy has a dependency on Newtonsoft.Json and Nlog2.

Examples
--------

Enough chat, lets see what it looks like:

    // create a client
    var client = HttpClient.Create("http://example.com/api");
    
    // get some companies!
    var companies = client.Get("companies").OnOK().As<List<Company>>();
  
    // upload a company, with validation error support
    client.Post(company, "companies")
        .On(HttpStatusCode.BadRequest, (List<ValidationError> errors) => {
          Console.WriteLine("Ruh Roh, you have {0} validation errors", errors.Count());
        })
        .On(HttpStatusCode.Created, () => Console.WriteLine("Holy moly you win!"));
    
    // update a company
    client.Put(company, "company/:id", new { id = "awesome-sauce" })
        .OnOK(() => Console.WriteLine("Company updated"));
        
    // change some state
    var company = Resource.Create("company/:id");
    client.Put(new { price = 3.1459 }, company.Id("Hooray!")).Is(HttpStatusCode.Created);
    
    // Asynchronous
    var company = Resource.Create("company/:id");
    var response = await client.Put(new { Name = "Awesome Startup"}, company.Id("1234"));
    var foo = ???