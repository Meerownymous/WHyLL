using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;
using Whyre.Request;

namespace Whyre.Request.Http1
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
                new RequestLine("GET", uri, new Version(1,1)).AsString(),
                headers,
                new MemoryStream()
            )
        )
        { }
    }
}

