﻿using Tonga;
using Tonga.Enumerable;
using WHyLL.Http.Request;
using WHyLL.Message;

namespace WHyLL.Http3.Request
{
    /// <summary>
    /// HTTP TRACE Request.
    /// </summary>
    public sealed class Trace(string url, IEnumerable<IPair<string, string>> headers) : 
        MessageEnvelope(
            new SimpleMessage(
                new RequestLine("TRACE", url, new Version(3, 0)),
                headers,
                new MemoryStream()
            )
        )
    {
        /// <summary>
        /// HTTP TRACE Request.
        /// </summary>
        public Trace(string url, params IPair<string, string>[] headers) : this(
            url, AsEnumerable._(headers)
        )
        { }
    }
}

