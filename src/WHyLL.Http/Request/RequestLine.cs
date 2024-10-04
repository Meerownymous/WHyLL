using Tonga;
using Tonga.Text;

namespace WHyLL.Http.Request
{
	/// <summary>
	/// The request line, first line of a http request.
	/// </summary>
	public sealed class RequestLine(IText method, IText resource, Version httpVersion) : 
		TextEnvelope(
			AsText._(() =>
				Joined._(
					"",
					Joined._(
						AsText._(" "),
						method,
						resource,
						AsText._($"HTTP/{httpVersion.ToString(2)}")
					)
				).AsString()
			)
		)
	{
        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestLine(string method) : this(method, new Version(1,1))
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestLine(string method, Version httpVersion) : this(
			new Strict(method, ignoreCase: true, "OPTIONS"),
			AsText._("*"),
			httpVersion
		)
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestLine(string method, Uri resource) : this(
			method, resource, new Version(1, 1)
		)
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestLine(string method, Uri resource, Version httpVersion) : this(
            new Method(method),
			AsText._(() =>
			{
				string result = resource.OriginalString;
				if (resource.IsAbsoluteUri)
				{
					result =
						$"{(resource.Scheme.Equals("file") ?
							""
							:
							$"{resource.Scheme}://{resource.Host}{(resource.Port != 80 ? $":{resource.Port}" : "")}"
						)}{resource.PathAndQuery}";
				}
				return result;
			}),
			httpVersion
		)
		{ }
    }
}

