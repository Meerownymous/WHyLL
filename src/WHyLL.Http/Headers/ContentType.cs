using Tonga.Map;

namespace WHyLL.Http.Headers
{
    /// <summary>
    /// Content-Type header.
    /// </summary>
    public sealed class ContentType(string contentType) : PairEnvelope<string,string>(
	    AsPair._(
		    "Content-Type",
		    () => contentType
	    )
    )
	{ }
}

