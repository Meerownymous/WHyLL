using Tonga.Map;

namespace WHyLL.Http.Headers
{
    /// <summary>
    /// Accept header.
    /// </summary>
    public sealed class Accept(string contentType) : PairEnvelope<string,string>(
	    AsPair._(
		    "Accept",
		    () => contentType
	    )
    )
	{ }
}

