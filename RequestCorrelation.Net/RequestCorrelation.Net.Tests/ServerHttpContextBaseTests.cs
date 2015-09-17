using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestCorrelation.Net.Tests
{
	[TestClass, ExcludeFromCodeCoverage]
	public class ServerHttpContextBaseTests
	{
		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfNoHeader()
		{
			var subject = new MyHttpContext(null);
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfHeaderNotAGuid()
		{
			var subject = new MyHttpContext("notaguid");
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfContextIsNull()
		{
			var subject = (HttpContextBase)null;
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfContextRequestIsNull()
		{
			var subject = new NullRequestContext();
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnTheGuidIfSet()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
			var subject = new MyHttpContext(expectedGuid.ToString("N"));
			Guid actualGuid;

			var result = subject.TryGetRequestId(out actualGuid);

			Assert.IsTrue(result);
			Assert.AreEqual(expectedGuid, actualGuid);
		}


		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfNoHeader()
		{
			var subject = new MyHttpContext(null);
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfHeaderNotAGuid()
		{
			var subject = new MyHttpContext("notaguid");
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfContextIsNull()
		{
			var subject = (HttpContextBase)null;
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfContextRequestIsNull()
		{
			var subject = new NullRequestContext();
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnTheGuidIfSet()
		{
			var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
			var subject = new MyHttpContext(expectedGuid.ToString("N"));

			var result = subject.GetRequestId();
			Assert.AreEqual(expectedGuid, result);
		}

		class MyHttpContext : HttpContextBase
		{
			public MyHttpContext(string header)
			{
				Request = new HeadersRequest(header);
			}

			public override HttpRequestBase Request { get; }
		}

		class NullRequestContext : HttpContextBase
		{
			public NullRequestContext()
			{
			}

			public override HttpRequestBase Request { get { return null; } }
		}
	}
}