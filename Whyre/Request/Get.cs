﻿using Tonga;
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
        public Get(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, AsEnumerable._(headers)
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, Version httpVersion, IEnumerable<IPair<string, string>> headers) : base(
            new SimpleMessage(
                new RequestLine("GET", uri, httpVersion).AsString(),
                headers,
                new MemoryStream()
            )
        )
        { }
    }
}

