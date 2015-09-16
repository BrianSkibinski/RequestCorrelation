namespace RequestRequest.Net
{
	public static class HttpHeaderKeys
	{
		/// <summary>
		/// The header to add for request id
		/// </summary>
		/// <remarks>
		/// while not an official standard, many cloud providers seem to use x-request-id for 
		/// the purpose of correlating client requests with server logs, including Heroku
		/// </remarks>
		public static string HttpRequestRequestHeader = "x-request-id";
	}
}