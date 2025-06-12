using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http2
{
    /// <summary>
    /// HTTP TRACE Request.
    /// </summary>
    public sealed class Trace(string url, IEnumerable<IPair<string, string>> headers) : 
        MessageEnvelope(
            new MessageWithInputs(
                new SimpleMessageInput(
                    new RequestPrologue("TRACE", url, new Version(2, 0)),
                    headers
                )
            )
            
        )
    {
        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(string url, params IPair<string, string>[] headers) : this(
            url, headers.AsEnumerable()
        )
        { }
    }
}

