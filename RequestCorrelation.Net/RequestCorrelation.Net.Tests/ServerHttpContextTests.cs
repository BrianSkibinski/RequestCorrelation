using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace RequestCorrelation.Tests
{
	[ExcludeFromCodeCoverage]
	public class ServerHttpContextTests
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
			var subject = (HttpContext)null;
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
			var subject = (HttpContext)null;
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

		[Fact]
		public void TryGetRequestIdShouldReturnFalseIfContextRequestIsNull()
		{
			var mockContext = new Mock<HttpContext>();
			var subject = mockContext.Object;

			var result = subject.TryGetRequestId(out _);
			Assert.False(result);
		}

		[Fact]
		public void GetRequestIdShouldReturnNewGuidIfContextRequestIsNull()
		{
			var mockContext = new Mock<HttpContext>();
			var subject = mockContext.Object;

			var result = subject.GetRequestId();
			Assert.NotEqual(Guid.Empty, result);
		}

		private static HttpContext GetSubject(string header)
		{
			var request = new Mock<HttpRequest>();

			var context = new Mock<HttpContext>();
			context.SetupGet(x => x.Request).Returns(request.Object);

			if (!string.IsNullOrEmpty(header))
			{
				request.SetupGet(x => x.Headers).Returns(new HeaderDictionary {{HttpHeaderKeys.HttpRequestIdHeader, header}});
			}
			else
			{
				request.SetupGet(x => x.Headers).Returns(new HeaderDictionary());
			}

			return context.Object;
		}
	}
}