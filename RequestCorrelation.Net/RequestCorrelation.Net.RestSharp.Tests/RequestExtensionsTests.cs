using System;
using System.Linq;
using RequestCorrelation.RestSharp;
using RestSharp;
using Xunit;

namespace RequestCorrelation.Net.RestSharp.Tests
{
	public class RequestExtensionsTests
	{
		[Fact]
		public void HeaderShouldBeAddedToRequest()
		{
			var expectedRequestId = Guid.NewGuid();
			var request = new RestRequest();

			request.AddRequestId(expectedRequestId);

			Assert.Equal(expectedRequestId.ToString("N"),
				request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "x-request-id")?.Value);
		}
	}
}
