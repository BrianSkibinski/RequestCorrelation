# RequestCorrelation

RequestCorrelation is a grouping of projects that will allow adding and receiving a request correlation header in x-request-id.  It is useful for tracing/logging across services so that you can correlate requests to responses in a multi-service environment (microservices/soa).

# Usage:
## Adding the RequestId to a WebRequest

	var request = WebRequest.Create("http://localhost/");
	request.AddRequestId(Guid.NewGuid());

## Adding the RequestId to a RestSharp 
	var request = new RestRequest("http://localhost");
	request.AddRequestId(Guid.NewGuid());

## Getting the RequestId on the Server
	Guid id = HttpContext.Current.GetRequestId()

## Getting the RequestId on the Server
	Guid id = Guid.Empty;
	if (HttpContext.Current.TryGetRequestId())
	{
		//use it
	}
	else 
	{ 
		//do something else
	}

## Getting the RequestId on the Server
	Guid id = Guid.Empty;
	if (Request.TryGetRequestId()) //Request assumes you're in a Controller/ApiController
	{
		//use it
	}
	else 
	{ 
		//do something else
	}


TODO: Nuget stuff