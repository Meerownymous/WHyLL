using System.Net;
using Tonga.Text;
using WHyLL;
using WHyLL.Http.Response;
using WHyLL.Message;

namespace WHyLL.Http.Response
{
	/// <summary>
	/// The response line, first line of a http response.
	/// </summary>
	public sealed class ResponseLine(int statusCode, Version httpVersion) : 
		TextEnvelope(
			AsText._(
				$"HTTP/{httpVersion.Major}.{httpVersion.Minor} {statusCode} {Enum.GetName(typeof(HttpStatusCode), statusCode)}"
			)
		)
	{
        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponseLine(int statusCode) : this(
			statusCode, new Version(1, 1)
		)
		{ }
        
		/// <summary>
		/// The response line, first line of a http response.
		/// </summary>
		public ResponseLine(HttpStatusCode statusCode) : this(
			Convert.ToInt32(statusCode), new Version(1, 1)
		)
		{ }
    }
}

public static class ResponseLineSmarts
{
	public static IMessage ResponseLine(this IMessage msg, HttpStatusCode statusCode) => 
		msg.With(new ResponseLine(statusCode));
	
	public static IMessage ResponseLine(this IMessage msg, int statusCode) => 
		msg.With(new ResponseLine(statusCode));
	
	public static IMessage ResponseLine(this IMessage msg, int statusCode, Version httpVersion) => 
		msg.With(new ResponseLine(statusCode, httpVersion));
}