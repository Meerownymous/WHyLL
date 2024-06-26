﻿using Tonga.Map;

namespace WHyLL.Http.Headers
{
    /// <summary>
    /// Token Authorization header.
    /// </summary>
    public sealed class BearerTokenAuth : PairEnvelope<string,string>
	{
        /// <summary>
        /// Token Authorization header.
        /// </summary>
        public BearerTokenAuth(string token) : base(
            AsPair._(
				"Authorization",
				() => $"Bearer {token}"
			)
		)
		{ }
	}
}

