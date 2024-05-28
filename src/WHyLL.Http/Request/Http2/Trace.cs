using Tonga;
using Tonga.Enumerable;
using WHyLL.Http.Request;
using WHyLL.Message;

namespace WHyLL.Http2.Request
{
    /// <summary>
    /// HTTP TRACE Request.
    /// </summary>
    public sealed class Trace : MessageEnvelope
    {
        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(Uri uri, params IPair<string, string>[] headers) : this(
            uri, AsEnumerable._(headers)
        )
        { }

        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(Uri uri, IEnumerable<IPair<string, string>> headers) : base(
            new SimpleMessage(
                new RequestLine("TRACE", uri, new Version(2, 0)),
                headers,
                new MemoryStream()
            )
        )
        { }
    }
}

