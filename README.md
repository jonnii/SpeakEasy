Resticle
========

**PLEASE NOTE THIS IS A WORK IN PROGRESS**
**IF YOU HAVE FEEDBACK PLEASE EMAIL ME**
**me@[mygithubusername].com**

This is a super simple library to talk to restful web services, the kind you can easily create using
asp.net mvc webapi. It's heavily inspired by [RestSharp](http://restsharp.org) and therefore licensed
the same. It exists primarily to scratch and itch I had that RestSharp couldn't and if you find it useful
then great!

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

    install-package Resticle
    
Examples
--------

    // create a client
    var client = new RestClient("http://example.com/api");
    
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