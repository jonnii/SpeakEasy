SpeakEasy
=========

SpeakEasy is a library for working with web apis in the language they speak best, http. It's heavily 
inspired by jQuery and RestSharp, so if you've used either of those libraries you should feel right
at home.

Show me the goods
=================

````
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
    
// make an asynchronous request
var response = await client.GetAsync("companies/:id", new { id = 5 })
response.OnOk(UpdateCompaniesCallback)
````

For more code samples check out the [features](README.md#features) section below.

There are also code samples and integration tests available in the `src\` directory which
show additional usage patterns.

How do I get it?
================

If you aren't using nuget, then shame on you. If you are, then installation is as simple as:

    # install the stable version
    install-package SpeakEasy
	
    # install the pre-release version
    install-package SpeakEasy -pre

Alternative Choices
===================

There are plenty of alternative choices for an http client out there which you should take a look at before
choosing SpeakEasy.

 * RestSharp (http://restsharp.org/)
 * Microsoft's HttpClient (http://msdn.microsoft.com/en-us/library/system.net.http.httpclient.aspx)
 * EasyHttp (https://github.com/hhariri/EasyHttp)

Builds
======

Builds are managed by code better. (http://teamcity.codebetter.com/login.html)

Contributing
============

I'd love if you could contribute. You could:

 * Add serializers.
 * Add authentication providers (oauth?)
 * Write examples and documentation!

However, if you're going to contribute please make sure you write tests for any new functionality.
Adding an example is also great!

Contributors
============

 * @jonnii (https://github.com/jonnii) [![endorse](http://api.coderwall.com/jonnii/endorsecount.png)](http://coderwall.com/jonnii)

Features
========

## Creating a client

````
// create a client with the default settings
var client = HttpClient.Create("http://example.com/api");
````