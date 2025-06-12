using Tonga.Map;

namespace WHyLL.Http.Headers;

/// <summary>
/// Basic Authorization header.
/// </summary>
public sealed class BasicAuth(string user, string password) : PairEnvelope<string,string>(
		"Authorization"
			.AsPair(
				() => $"Basic {new Tonga.Text.Base64Encoded($"{user}:{password}").Str()}"
			)
);