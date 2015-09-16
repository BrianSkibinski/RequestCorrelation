using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestRequest.Net.Tests
{
	[TestClass, ExcludeFromCodeCoverage]
	public class ServerHttpRequestTests
	{
		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfNoHeader()
		{
			var subject = GetSubject(null);
			Guid resultGuid;
			var result = subject.TryGetRequestId(out resultGuid);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfHeaderNotAGuid()
		{
			using (ShimsContext.Create())
			{
				var subject = GetSubject("notaguid");
				Guid resultGuid;
				var result = subject.TryGetRequestId(out resultGuid);
				Assert.IsFalse(result);
			}
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnFalseIfContextIsNull()
		{
			using (ShimsContext.Create())
			{
				var subject = (HttpRequest) null;
				Guid resultGuid;
				var result = subject.TryGetRequestId(out resultGuid);
				Assert.IsFalse(result);
			}
		}

		[TestMethod]
		public void TryGetRequestIdShouldReturnTheGuidIfSet()
		{
			using (ShimsContext.Create())
			{
				var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
				var subject = GetSubject(expectedGuid.ToString("N"));
				Guid actualGuid;

				var result = subject.TryGetRequestId(out actualGuid);

				Assert.IsTrue(result);
				Assert.AreEqual(expectedGuid, actualGuid);
			}
		}


		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfNoHeader()
		{
			var subject = GetSubject(null);
			var result = subject.GetRequestId();
			Assert.AreNotEqual(Guid.Empty, result);
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfHeaderNotAGuid()
		{
			using (ShimsContext.Create())
			{
				var subject = GetSubject("notaguid");
				var result = subject.GetRequestId();
				Assert.AreNotEqual(Guid.Empty, result);
			}
		}

		[TestMethod]
		public void GetRequestIdShouldReturnNewGuidIfContextIsNull()
		{
			using (ShimsContext.Create())
			{
				var subject = (HttpRequest) null;
				var result = subject.GetRequestId();
				Assert.AreNotEqual(Guid.Empty, result);
			}
		}

		[TestMethod]
		public void GetRequestIdShouldReturnTheGuidIfSet()
		{
			using (ShimsContext.Create())
			{
				var expectedGuid = new Guid("88887777-6666-5555-4444-333322221111");
				var subject = GetSubject(expectedGuid.ToString("N"));

				var result = subject.GetRequestId();
				Assert.AreEqual(expectedGuid, result);
			}
		}

		private static HttpRequest GetSubject(string header)
		{
			var request = new HttpRequest("file", "http://localhost/", string.Empty);
			if (!string.IsNullOrEmpty(header))
			{
				ShimHttpRequest.AllInstances.HeadersGet = httpRequest =>
				{
					var collection = new NameValueCollection {{HttpHeaderKeys.HttpRequestRequestHeader, header}};
					return collection;
				};
			}

			return request;
		}
	}
}
