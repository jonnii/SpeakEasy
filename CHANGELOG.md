# CHANGELOG

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