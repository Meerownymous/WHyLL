using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;
using Whyre.Request;

namespace Whyre
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get : MessageEnvelope
    {
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, params IPair<string, string>[] parts) : this(
            uri, AsEnumerable._(parts)
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, IEnumerable<IPair<string, string>> parts) : base(
            new SimpleMessage(
                new RequestLine("get", uri).AsString(),
                parts,
                new MemoryStream()
            )
        )
        { }
    }
}

