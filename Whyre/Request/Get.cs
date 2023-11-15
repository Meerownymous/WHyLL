using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;
using Whyre.Request;

namespace Whyre
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get : RequestEnvelope
    {
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(IRequest via, Uri uri, params IPair<string, string>[] parts) : this(
            via,
            Joined._(parts,
                new Scheme(uri.Scheme),
                new Host(uri.Host),
                new Port(uri.Port),
                new Parts.Path(uri.PathAndQuery.Substring(uri.PathAndQuery.Length - uri.Query.Length)),
                new Query(uri.Query)
            )
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(IRequest via, IPair<string,string> first, params IPair<string, string>[] parts) : this(
            via,
            Mapped._(
                part => AsRequestInput._(part),
                Joined._(parts, first)
            )
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(IRequest via, IEnumerable<IPair<string, string>> parts) : this(
            via,
            Mapped._(
                part => AsRequestInput._(part),
                parts
            )
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(IRequest via, params IRequestInput[] parts) : this(via, AsEnumerable._(parts))
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(IRequest via, IEnumerable<IRequestInput> parts) : base(
            new SimpleRequest(via, new RequestLine("get"), parts)
        )
        { }
    }
}

