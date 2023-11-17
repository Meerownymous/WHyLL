using Tonga;
using Tonga.Text;

namespace WHyLL.Request
{
	/// <summary>
	/// The request line, first line of a http request.
	/// </summary>
	public sealed class RequestLine : TextEnvelope
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
			AsText._(
				$"{(resource.Scheme.Equals("file") ?
					""
					:
					$"{resource.Scheme}://{resource.Host}{(resource.Port != 80 ? $":{resource.Port}" : "")}"
				)}{resource.PathAndQuery}"
			),
			httpVersion
		)
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        private RequestLine(IText method, IText resource, Version httpVersion) : base(
			AsText._(() =>
				Joined._(
					"",
					Joined._(
						AsText._(" "),
						method,
						resource,
						AsText._($"HTTP/{httpVersion.ToString(2)}")
					),
					AsText._("\r\n")
				).AsString()
            )
        )
		{ }
    }
}

