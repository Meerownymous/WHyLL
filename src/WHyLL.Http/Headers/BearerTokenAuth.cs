﻿using Tonga.Map;

namespace WHyLL.Http.Headers
{
    /// <summary>
    /// Token Authorization header.
    /// </summary>
    public sealed class BearerTokenAuth(string token) : PairEnvelope<string,string>(
	    AsPair._(
		    "Authorization",
		    () => $"Bearer {token}"
	    )
	)
	{ }
}

