using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RequestCorrelation.Net
{
	public static class Server
	{
		/// <summary>
		/// Try to get the requestid from the x-request-id header of the request
		/// </summary>
		/// <param name="request">The request</param>
		/// <param name="requestId">The object to set if the request has the header</param>
		/// <returns>False if the request is null or doesn't contain the header.</returns>
		public static bool TryGetRequestId(this HttpRequestBase request, out Guid requestId)
		{
			requestId = Guid.Empty;
			if (request == null) return false;
			if (!request.Headers.AllKeys.Contains(HttpHeaderKeys.HttpRequestIdHeader)) return false;

			return Guid.TryParse(request.Headers[HttpHeaderKeys.HttpRequestIdHeader], out requestId);
		}

		/// <summary>
		/// Get the request id from the request header.  If it doesn't exist or the request is null, return  <see cref="Guid.NewGuid"/>.
		/// </summary>
		/// <param name="request">The request to get the request id from</param>
		/// <returns>A Guid, either from the header or a newly generated one</returns>
		public static Guid GetRequestId(this HttpRequestBase request)
		{
			Guid requestId;
			return TryGetRequestId(request, out requestId) ? requestId : Guid.NewGuid();
		}

		/// <summary>
		/// Try to get the requestid from the x-request-id header of the request
		/// </summary>
		/// <param name="currentContext">The current HTTPContext</param>
		/// <param name="requestId">The object to set if the request has the header</param>
		/// <returns>False if the context or request is null or if the request doesn't contain the header.</returns>
		public static bool TryGetRequestId(this HttpContextBase currentContext, out Guid requestId)
		{
			requestId = Guid.Empty;
			if (currentContext == null) return false;

			return TryGetRequestId(currentContext.Request, out requestId);
		}

		/// <summary>
		/// Get the request id from the request header.  If it doesn't exist or the request is null, return <see cref="Guid.NewGuid"/>.
		/// </summary>
		/// <param name="currentContext">The current HttpContext in which to get the request's headers from</param>
		/// <returns>A Guid, either from the header or a newly generated one</returns>
		public static Guid GetRequestId(this HttpContextBase currentContext)
		{
			Guid requestId;
			return TryGetRequestId(currentContext, out requestId) ? requestId : Guid.NewGuid();
		}

		/// <summary>
		/// Try to get the requestid from the x-request-id header of the request
		/// </summary>
		/// <param name="request">The request</param>
		/// <param name="requestId">The object to set if the request has the header</param>
		/// <returns>False if the request is null or doesn't contain the header.</returns>
		public static bool TryGetRequestId(this HttpRequest request, out Guid requestId)
		{
			requestId = Guid.Empty;
			if (request == null) return false;
			if (!request.Headers.AllKeys.Contains(HttpHeaderKeys.HttpRequestIdHeader)) return false;

			return Guid.TryParse(request.Headers[HttpHeaderKeys.HttpRequestIdHeader], out requestId);
		}

		/// <summary>
		/// Get the request id from the request header.  If it doesn't exist or the request is null, return  <see cref="Guid.NewGuid"/>.
		/// </summary>
		/// <param name="request">The request to get the request id from</param>
		/// <returns>A Guid, either from the header or a newly generated one</returns>
		public static Guid GetRequestId(this HttpRequest request)
		{
			Guid requestId;
			return TryGetRequestId(request, out requestId) ? requestId : Guid.NewGuid();
		}

		/// <summary>
		/// Try to get the requestid from the x-request-id header of the request
		/// </summary>
		/// <param name="currentContext">The current HTTPContext</param>
		/// <param name="requestId">The object to set if the request has the header</param>
		/// <returns>False if the context or request is null or if the request doesn't contain the header.</returns>
		public static bool TryGetRequestId(this HttpContext currentContext, out Guid requestId)
		{
			requestId = Guid.Empty;
			if (currentContext == null) return false;

			return TryGetRequestId(currentContext.Request, out requestId);
		}

		/// <summary>
		/// Get the request id from the request header.  If it doesn't exist or the request is null, return  <see cref="Guid.NewGuid"/>.
		/// </summary>
		/// <param name="currentContext">The current HttpContext in which to get the request's headers from</param>
		/// <returns>A Guid, either from the header or a newly generated one</returns>
		public static Guid GetRequestId(this HttpContext currentContext)
		{
			Guid requestId;
			return TryGetRequestId(currentContext, out requestId) ? requestId : Guid.NewGuid();
		}


		/// <summary>
		/// Try to get the requestid from the x-request-id header of the request
		/// </summary>
		/// <param name="request">The current HttpRequestMessage</param>
		/// <param name="requestId">The object to set if the request has the header</param>
		/// <returns>False if the context or request is null or if the request doesn't contain the header.</returns>
		public static bool TryGetRequestId(this HttpRequestMessage request, out Guid requestId)
		{
			requestId = Guid.Empty;
			if (request == null) return false;

			IEnumerable <string> headerValues;
			if (!request.Headers.TryGetValues(HttpHeaderKeys.HttpRequestIdHeader, out headerValues))
			{
				if (headerValues == null) return false;
			}

			var requestIdHeader = headerValues.FirstOrDefault();
			if (requestIdHeader == null)
			{
				return false;
			}

			return Guid.TryParse(requestIdHeader, out requestId);
		}

		/// <summary>
		/// Get the request id from the request header.  If it doesn't exist or the request is null, return  <see cref="Guid.NewGuid"/>.
		/// </summary>
		/// <param name="currentContext">The current HttpContext in which to get the request's headers from</param>
		/// <returns>A Guid, either from the header or a newly generated one</returns>
		public static Guid GetRequestId(this HttpRequestMessage currentContext)
		{
			Guid requestId;
			return TryGetRequestId(currentContext, out requestId) ? requestId : Guid.NewGuid();
		}
	}

}
