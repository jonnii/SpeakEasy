# CHANGELOG

## Next

  * .NET 4.6.1 Only [robertcoltheart]

## 0.3.6.0

  * When making a POST/PUT/PATCH with an object body and segments then
    extra segment values which do not merge into the URL will be added
	as query string parameters.

## 0.3.5.0

  * Fix a bug with query params not being added to a resource when a body
    is not given for POST/PUT/Patch, this is inconsistent with how GET 
	requests work.

## 0.3.4.0

  * Change query string parameter format from "s", to "o", this is to 
    fix problems with dates being passed as query string parameters and
	not being interpreted as UTC

## 0.3.3.0

  * Add overload to http response to access the internal http state.

## 0.3.2.0

  * Fix a bug with the way mime documents were formatted for form data

## 0.3.1.0

  * Bug fix for uploading files with resource parameters

## 0.3.0.0

  * Add ability to customize array formatting, this changes the default array 
    formatting from `?property=1,2,3` to `?property=1&property=2&property=3` to be 
    more consistent with both WebApi and jQuery.

## 0.2.5.0

  * Add generic As method to http response handler

## 0.2.4.0

  * Enable Dynamic in SimpleJson

## 0.2.3.0

  * Fix bug with `On(StatusCode)` not throwing exceptions when it should

## 0.2.2.0

  * Move construction to constructor from static method
  * Update simplejson
  * Add the ability to change the default handling the cookie container, no longer need to override BeforeRequest

## 0.2.1.0

  * Add overloads for On/As to support numeric status codes

## 0.2.0.1

  * Fix a bug with file uploading

## 0.2.0.0
  
  * retarget .net 4.5
  * remove support for silverlight
  * it's a brave new async world

## 0.1.14.0

  * remove dependency on json.net, serialization is now handled by simplejson

## 0.1.13.4

  * update packages

## 0.1.13.2

  * when creating a query string with a datetime format it using an invariant culture

## 0.1.13.1

  * update packages

## 0.1.13.1

  * [BUG] Proxy was being overwritten when not specified causing requests not to show up in fiddler.