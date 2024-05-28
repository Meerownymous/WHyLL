using Tonga.Map;

namespace WHyLL.Http.Headers
{
    /// <summary>
    /// Content-Type header.
    /// </summary>
    public sealed class ContentType : PairEnvelope<string,string>
	{
        /// <summary>
        /// Content-Type header.
        /// </summary>
        public ContentType(string contentType) : base(
            AsPair._(
                "Content-Type",
				() => contentType
			)
		)
		{ }
	}
}

