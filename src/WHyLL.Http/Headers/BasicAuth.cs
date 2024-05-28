using System;
using Tonga.Text;
using Tonga.Map;

namespace WHyLL.Http.Headers
{
	/// <summary>
	/// Basic Authorization header.
	/// </summary>
	public sealed class BasicAuth : PairEnvelope<string,string>
	{
        /// <summary>
        /// Basic Authorization header.
        /// </summary>
        public BasicAuth(string user, string password) : base(
            AsPair._(
				"Authorization",
				() => $"Basic {new TextAsBase64($"{user}:{password}").AsString()}"
			)
		)
		{ }
	}
}

