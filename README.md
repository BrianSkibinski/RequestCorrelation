# RequestCorrelation

RequestCorrelation is a grouping of projects that will allow adding and receiving a request correlation header in x-request-id.  It is useful for tracing/logging across services so that you can correlate requests to responses in a multi-service environment (microservices/soa).

Currently we only have two libraries, one for regular .net and one that extends RestSharp.  They don't depend on each other so you can use either/both.

#Install
Get it from nuget:

[PM> Install-Package RequestCorrelation.Net](https://www.nuget.org/packages/RequestCorrelation.Net/)

OR

[PM> Install-Package RequestCorrelation.Net.RestSharp](https://www.nuget.org/packages/RequestCorrelation.Net.RestSharp/)

# Usage:
All of the GetRequestId methods will return the header if present and a Guid, otherwise will return a new randomly generated Guid using Guid.NewGuid.  Any x-request-id header that can be parsed by Guid.TryParse will be returned.  If you need to know if the header was there, use the TryGetRequestId methods.

## Adding the RequestId to a WebRequest
```C#
var request = WebRequest.Create("http://localhost/");
request.AddRequestId(Guid.NewGuid());
```

## Adding the RequestId to a RestSharp 
```C#
var request = new RestRequest("http://localhost");
request.AddRequestId(Guid.NewGuid());
```

## Getting the RequestId or a new Guid on the Server
```C#
Guid id = HttpContext.Current.GetRequestId()
```

## Getting the RequestId on the Server
```C#
Guid id = Guid.Empty;
if (HttpContext.Current.TryGetRequestId(out id))
{
//use it
} else { 
//do something else
}
```

## Getting the RequestId on the Server in MVC (works the same way in WebApi)
```C#
[HttpGet]
public virtual ActionResult Index()
{
	var requestId = Request.GetRequestId();
	...
}
```

#Known issues:
We're only building a .Net 4.5 version right now but there's not really a reason it couldn't support others.  I accept pull requests :)
