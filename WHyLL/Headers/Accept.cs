using System;
using Tonga.Text;
using Tonga.Map;

namespace WHyLL.Headers
{
	/// <summary>
	/// Accept header.
	/// </summary>
	public sealed class Accept : PairEnvelope<string,string>
	{
        /// <summary>
        /// Accept header.
        /// </summary>
        public Accept(string contentType) : base(
            AsPair._(
                "Accept",
				() => contentType
			)
		)
		{ }
	}
}

