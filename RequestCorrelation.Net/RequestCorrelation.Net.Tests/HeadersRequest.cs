using System.Collections.Specialized;
using System.Web;

namespace RequestRequest.Net.Tests
{
	public class HeadersRequest : HttpRequestBase
	{
		public HeadersRequest(string header)
		{
			Headers = new NameValueCollection();
			if (!string.IsNullOrWhiteSpace(header))
				Headers.Add("x-request-id", header);
		}

		public override NameValueCollection Headers { get; }
	}
}