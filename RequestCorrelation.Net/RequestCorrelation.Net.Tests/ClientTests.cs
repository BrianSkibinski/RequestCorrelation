using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Xunit;

namespace RequestCorrelation.Tests
{
	[ExcludeFromCodeCoverage]
	public class ClientTests
	{
		[Fact]
		public void WebRequestShouldThrowIfNull()
		{
			var subject = (WebRequest)null;
			Assert.Throws<ArgumentNullException>(() => subject.AddRequestId(Guid.Empty));
		}

		[Fact]
		public void WebRequestShouldAddRequestIdToHeader()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(expectedGuid);
			Assert.Equal(subject.Headers["x-request-id"], expectedGuid.ToString("N"));

		}

		[Fact]
		public void WebRequestShouldAddRequestIdToHeaderAndOverwriteIfSet()
		{
			var originalGuid = new Guid("11112222-3333-4444-5555-666677778888");
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(originalGuid);
			subject.AddRequestId(expectedGuid);
			Assert.Equal(subject.Headers["x-request-id"], expectedGuid.ToString("N"));
		}

		[Fact]
		public void WebRequestShouldThrowIfRequestIdAlreadySet()
		{
			var originalGuid = new Guid("11112222-3333-4444-5555-666677778888");
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(originalGuid);

			Assert.Throws<ArgumentException>(() => subject.AddRequestId(expectedGuid, false));
		}

		[Fact]
		public void WebRequestShouldThrowIfRequestIdAlreadySetAndEqual()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(expectedGuid);
			subject.AddRequestId(expectedGuid, false);
			Assert.Equal(subject.Headers["x-request-id"], expectedGuid.ToString("N"));
		}
	}
}