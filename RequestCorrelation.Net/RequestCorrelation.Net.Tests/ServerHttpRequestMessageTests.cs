using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Xunit;

namespace RequestCorrelation.Tests
{
	[ExcludeFromCodeCoverage]
	public class ServerHttpRequestMessageTests
	{
		[Fact]
		public void TryGetRequestIdShouldReturnFalseIfNoHeader()
		{
			var subject = GetSubject(null);
			var result = subject.TryGetRequestId(out _);
			Assert.False(result);
		}

		[Fact]
		public void TryGetRequestIdShouldReturnFalseIfHeaderNotAGuid()
		{
			var subject = GetSubject("notaguid");
			var result = subject.TryGetRequestId(out _);
			Assert.False(result);
		}

		[Fact]
		public void TryGetRequestIdShouldReturnFalseIfContextIsNull()
		{
			var subject = (HttpRequestMessage)null;
			var result = subject.TryGetRequestId(out _);
			Assert.False(result);
		}

		[Fact]
		public void TryGetRequestIdShouldReturnTheGuidIfSet()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
			var subject = GetSubject(expectedGuid.ToString("N"));

			var result = subject.TryGetRequestId(out var actualGuid);

			Assert.True(result);
			Assert.Equal(expectedGuid, actualGuid);
		}


		[Fact]
		public void GetRequestIdShouldReturnNewGuidIfNoHeader()
		{
			var subject = GetSubject(null);
			var result = subject.GetRequestId();
			Assert.NotEqual(Guid.Empty, result);
		}

		[Fact]
		public void GetRequestIdShouldReturnNewGuidIfHeaderNotAGuid()
		{
				var subject = GetSubject("notaguid");
				var result = subject.GetRequestId();
				Assert.NotEqual(Guid.Empty, result);
		}

		[Fact]
		public void GetRequestIdShouldReturnNewGuidIfContextIsNull()
		{
				var subject = (HttpRequestMessage)null;
				var result = subject.GetRequestId();
				Assert.NotEqual(Guid.Empty, result);
		}

		[Fact]
		public void GetRequestIdShouldReturnTheGuidIfSet()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
			var subject = GetSubject(expectedGuid.ToString("N"));

			var result = subject.GetRequestId();
			Assert.Equal(expectedGuid, result);
		}

		private static HttpRequestMessage GetSubject(string header)
		{
			var request = new HttpRequestMessage();
			if (!string.IsNullOrEmpty(header))
			{
				request.Headers.Add(HttpHeaderKeys.HttpRequestIdHeader, header);
			}

			return request;
		}
	}
}