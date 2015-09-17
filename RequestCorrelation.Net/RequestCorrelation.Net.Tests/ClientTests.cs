using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestCorrelation.Tests
{
	[TestClass, ExcludeFromCodeCoverage]
	public class ClientTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WebRequestShouldThrowIfNull()
		{
			var subject = (WebRequest)null;
			subject.AddRequestId(Guid.Empty);
		}

		[TestMethod]
		public void WebRequestShouldAddRequestIdToHeader()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(expectedGuid);
			Assert.AreEqual(subject.Headers["x-request-id"], expectedGuid.ToString("N"));

		}

		[TestMethod]
		public void WebRequestShouldAddRequestIdToHeaderAndOverwriteIfSet()
		{
			var originalGuid = new Guid("11112222-3333-4444-5555-666677778888");
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(originalGuid);
			subject.AddRequestId(expectedGuid);
			Assert.AreEqual(subject.Headers["x-request-id"], expectedGuid.ToString("N"));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void WebRequestShouldThrowIfRequestIdAlreadySet()
		{
			var originalGuid = new Guid("11112222-3333-4444-5555-666677778888");
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(originalGuid);
			subject.AddRequestId(expectedGuid, false);
		}

		[TestMethod]
		public void WebRequestShouldThrowIfRequestIdAlreadySetAndEqual()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");

			var subject = WebRequest.Create("http://google.com");
			subject.AddRequestId(expectedGuid);
			subject.AddRequestId(expectedGuid, false);
			Assert.AreEqual(subject.Headers["x-request-id"], expectedGuid.ToString("N"));
		}
	}
}