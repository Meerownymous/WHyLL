using System.Net;
using WHyLL.MessageInput;

namespace WHyLL.Http.MessageInput;

    /// <summary>
    /// The response line, first line of a http response.
    /// </summary>
    public sealed class ResponsePrologue(int statusCode, Version httpVersion) : 
        MessageInputEnvelope(
            new PrologueInput(
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
        public ResponsePrologue(HttpStatusCode statusCode) : this(
            Convert.ToInt32(statusCode), new Version(1, 1)
        )
        { }
		
        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponsePrologue(int statusCode) : this(
            statusCode, new Version(1, 1)
        )
        { }
    }