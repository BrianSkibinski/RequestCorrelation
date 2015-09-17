# RequestCorrelation

RequestCorrelation is a grouping of projects that will allow adding and receiving a request correlation header in x-request-id.  It is useful for tracing/logging across services so that you can correlate requests to responses in a multi-service environment (microservices/soa).

#Install
Get it from nuget:
PM> Install-Package RequestCorrelation.Net

OR

PM> Install-Package RequestCorrelation.Net.RestSharp

# Usage:
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

## Getting the RequestId on the Server
```C#
Guid id = HttpContext.Current.GetRequestId()
```

## Getting the RequestId on the Server
```C#
Guid id = Guid.Empty;
if (HttpContext.Current.TryGetRequestId())
{
//use it
} else { 
	//do something else
}
```

## Getting the RequestId on the Server
```C#
Guid id = Guid.Empty;
if (Request.TryGetRequestId()) //Request assumes you're in a Controller/ApiController
{
	//use it
} else { 
	//do something else
}
```

#Known issues:
We're only building a .Net 4.5 version right now but there's not really a reason it couldn't support others.  I accept pull requests :)
