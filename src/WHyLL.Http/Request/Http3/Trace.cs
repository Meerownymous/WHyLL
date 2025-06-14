﻿using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Http.Request.Http3;

/// <summary>
/// HTTP TRACE Request.
/// </summary>
public sealed class Trace(string url, IEnumerable<IPair<string, string>> headers) : 
    MessageEnvelope(
        new SimpleMessage(
            new RequestPrologue("TRACE", url, new Version(3, 0)),
            headers,
            new MemoryStream()
        )
    )
{
    /// <summary>
    /// HTTP TRACE Request.
    /// </summary>
    public Trace(string url, params IPair<string, string>[] headers) : this(
        url, headers.AsEnumerable()
    )
    { }
}