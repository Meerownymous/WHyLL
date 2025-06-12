using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP TRACE Request.
    /// </summary>
    public sealed class Trace(string url, Version httpVersion, IEnumerable<IPair<string, string>> headers) : 
        MessageEnvelope(
            new MessageWithInputs(
                new SimpleMessageInput(
                    new RequestPrologue("TRACE", url, httpVersion),
                    headers
                )
            )
        )
    {
        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(string url, params IPair<string, string>[] headers) : this(
            url, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(string url, Version httpVersion, params IPair<string, string>[] headers) : this(
            url, httpVersion, headers.AsEnumerable()
        )
        { }

        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(string url, IEnumerable<IPair<string, string>> headers) : this(
            url, new Version(1,1), headers
        )
        { }
    }
}

