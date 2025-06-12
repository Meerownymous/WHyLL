using Tonga.Map;

namespace WHyLL.Http.Headers;

/// <summary>
/// Accept header.
/// </summary>
public sealed class Accept(string contentType) : PairEnvelope<string, string>(
	"Accept".AsPair(() => contentType));


