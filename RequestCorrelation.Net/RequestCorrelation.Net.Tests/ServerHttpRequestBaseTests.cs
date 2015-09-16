using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestRequest.Net.Tests
{
	[TestClass, ExcludeFromCodeCoverage]
	public class ServerHttpRequestBaseTests
	{
		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfNoHeader()
		{
			var subject = new HeadersRequest(null);
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfHeaderNotAGuid()
		{
			var subject = new HeadersRequest("notaguid");
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfRequestIsNull()
		{
			var subject = (HttpRequestBase)null;
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnTheGuidIfSet()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
			var subject = new HeadersRequest(expectedGuid.ToString("N"));
			Guid actualGuid;

			var result = subject.TryGetRequestId(out actualGuid);

			Assert.IsTrue(result);
			Assert.AreEqual(expectedGuid, actualGuid);
		}


		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfNoHeader()
		{
			var subject = new HeadersRequest(null);
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfHeaderNotAGuid()
		{
			var subject = new HeadersRequest("notaguid");
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfRequestIsNull()
		{
			var subject = (HttpRequestBase)null;
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnTheGuidIfSet()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
			var subject = new HeadersRequest(expectedGuid.ToString("N"));

			var result = subject.GetRequestId();
			Assert.AreEqual(expectedGuid, result);
		}
	}
}