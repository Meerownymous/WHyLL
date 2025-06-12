using System.Net;
using Tonga.Text;
using WHyLL;
using WHyLL.Http.Response;
using WHyLL.Message;
using WHyLL.Prologue;

namespace WHyLL.Http.Response
{
	/// <summary>
	/// The response line, first line of a http response.
	/// </summary>
	public sealed class ResponsePrologue(int statusCode, Version httpVersion) : 
		PrologueEnvelope(
			new AsPrologue(
				[
					$"HTTP/{httpVersion.Major}.{httpVersion.Minor}",
					statusCode.ToString(),
					Enum.GetName(typeof(HttpStatusCode), statusCode)
				]
			)
		)
	{
        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponsePrologue(int statusCode) : this(
			statusCode, new Version(1, 1)
		)
		{ }
        
		/// <summary>
		/// The response line, first line of a http response.
		/// </summary>
		public ResponsePrologue(HttpStatusCode statusCode) : this(
			Convert.ToInt32(statusCode), new Version(1, 1)
		)
		{ }
    }
}

public static class ResponseLineSmarts
{
	public static IMessage ResponseLine(this IMessage msg, HttpStatusCode statusCode) => 
		msg.With(new ResponsePrologue(statusCode));
	
	public static IMessage ResponseLine(this IMessage msg, int statusCode) => 
		msg.With(new ResponsePrologue(statusCode));
	
	public static IMessage ResponseLine(this IMessage msg, int statusCode, Version httpVersion) => 
		msg.With(new ResponsePrologue(statusCode, httpVersion));
}