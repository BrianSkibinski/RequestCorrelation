using System;

namespace RequestCorrelation.Net.RestSharp
{
    public static class RequestExtensions
    {
		/// <summary>
		/// Adds a request id to the web request
		/// </summary>
		/// <param name="request">The request to add it to</param>
		/// <param name="requestId">The request id</param>
		/// <exception cref="ArgumentNullException">The request is null</exception>
		/// <exception cref="ArgumentException">Request already has a request id and the overwrite flag is false</exception>
		public static void AddRequestId(this RestRequest request, Guid requestId)
	    {
		    request.AddHeader("x-request-id", requestId.ToString("N"));
		}
    }
}
