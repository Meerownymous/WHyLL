using Tonga.Map;

namespace WHyLL.Http.Headers;

/// <summary>
/// Content-Type header.
/// </summary>
public sealed class ContentType(string contentType) : PairEnvelope<string, string>(
	"Content-Type".AsPair(() => contentType)
);

