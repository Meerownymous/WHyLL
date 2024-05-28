using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Http.Request
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
            uri, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, AsEnumerable._(headers)
        )
        { }

        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(Uri uri, IEnumerable<IPair<string, string>> headers) : this(
            uri, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(Uri uri, Version httpVersion, IEnumerable<IPair<string, string>> headers) : base(
            new SimpleMessage(
                new RequestLine("TRACE", uri, httpVersion),
                headers,
                new MemoryStream()
            )
        )
        { }
    }
}

