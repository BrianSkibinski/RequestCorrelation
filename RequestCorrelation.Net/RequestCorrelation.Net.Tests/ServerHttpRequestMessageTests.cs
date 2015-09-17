using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestCorrelation.Tests
{
	[TestClass, ExcludeFromCodeCoverage]
	public class ServerHttpRequestMessageTests
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
				var subject = (HttpRequestMessage)null;
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
				var subject = (HttpRequestMessage)null;
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