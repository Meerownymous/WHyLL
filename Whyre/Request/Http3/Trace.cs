using Tonga;
using Tonga.Enumerable;
using Whyre.Message;

namespace Whyre.Request.Http3
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
                new RequestLine("TRACE", uri, new Version(3, 0)),
                headers,
                new MemoryStream()
            )
        )
        { }
    }
}

