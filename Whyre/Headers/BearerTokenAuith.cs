using System;
using Tonga.Text;
using Tonga.Map;

namespace Whyre.Headers
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

