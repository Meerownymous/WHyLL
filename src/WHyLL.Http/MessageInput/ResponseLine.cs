using System.Net;
using WHyLL.MessageInput;

namespace WHyLL.Http.MessageInput
{
    /// <summary>
    /// The response line, first line of a http response.
    /// </summary>
    public sealed class ResponseLine(int statusCode, Version httpVersion) : 
        MessageInputEnvelope(
            new FirstLineInput(
                $"HTTP/{httpVersion.Major}.{httpVersion.Minor} {statusCode} {Enum.GetName(typeof(HttpStatusCode), statusCode)}"
            )
        )
    {
		
        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponseLine(HttpStatusCode statusCode) : this(
            Convert.ToInt32(statusCode), new Version(1, 1)
        )
        { }
		
        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponseLine(int statusCode) : this(
            statusCode, new Version(1, 1)
        )
        { }
    }
}