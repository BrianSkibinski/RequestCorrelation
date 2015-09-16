using System;
using System.Linq;
using System.Net;

namespace RequestRequest.Net
{
	public static class Client
	{
		/// <summary>
		/// Adds a request id to the web request
		/// </summary>
		/// <param name="request">The request to add it to</param>
		/// <param name="requestId">The request id</param>
		/// <param name="overwriteIfExists">If false, and there is already a request id added to the request, throw an exception.  If set, overwrite the existing value</param>
		/// <exception cref="ArgumentNullException">The request is null</exception>
		/// <exception cref="ArgumentException">Request already has a request id and the overwrite flag is false</exception>
		public static void AddRequestId(this WebRequest request, Guid requestId, bool overwriteIfExists = true)
		{
			if (request == null) throw new ArgumentNullException("request", "The request cannot be null");
			if (request.Headers.AllKeys.Contains(HttpHeaderKeys.HttpRequestRequestHeader))
			{
				if (overwriteIfExists == false && request.Headers[HttpHeaderKeys.HttpRequestRequestHeader] != requestId.ToString("N"))
					throw new ArgumentException("Request already contains a header for request id (request id).  To overwrite, call this method with overwriteIfExists = true");

				request.Headers[HttpHeaderKeys.HttpRequestRequestHeader] = requestId.ToString("N");
			}
			else request.Headers.Add(HttpHeaderKeys.HttpRequestRequestHeader, requestId.ToString("N"));
		}
	}
}