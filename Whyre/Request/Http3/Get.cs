using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;
using Whyre.Request;

namespace Whyre.Request.Http2
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get : MessageEnvelope
    {
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, params IPair<string, string>[] headers) : this(
            uri, AsEnumerable._(headers)
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, IEnumerable<IPair<string, string>> headers) : base(
            new SimpleMessage(
                new RequestLine("GET", uri, new Version(2,0)).AsString(),
                headers,
                new MemoryStream()
            )
        )
        { }
    }
}

