using Tonga.Text;
using Tonga.Map;

namespace WHyLL.Http.Headers
{
	/// <summary>
	/// Basic Authorization header.
	/// </summary>
	public sealed class BasicAuth(string user, string password) : PairEnvelope<string,string>(
		AsPair._(
			"Authorization",
			() => $"Basic {new TextAsBase64($"{user}:{password}").AsString()}"
		)
	)
	{ }
}

