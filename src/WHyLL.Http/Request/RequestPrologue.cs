using Tonga;
using Tonga.Text;
using WHyLL.Prologue;

namespace WHyLL.Http.Request
{
	/// <summary>
	/// The request line, first line of a http request.
	/// </summary>
	public sealed class RequestPrologue(IText method, IText resource, Version httpVersion) : 
		PrologueEnvelope(
			() =>
				[
					method.Str(),
					resource.Str(),
					$"HTTP/{httpVersion.ToString(2)}"
				]
		)
	{
        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestPrologue(string method) : this(method, new Version(1,1))
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestPrologue(string method, Version httpVersion) : this(
			new Strict(method, ignoreCase: true, "OPTIONS"),
			"*".AsText(),
			httpVersion
		)
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestPrologue(string method, string url) : this(
			method, url, new Version(1, 1)
		)
		{ }

        /// <summary>
        /// The request line, first line of a http request.
        /// </summary>
        public RequestPrologue(string method, string url, Version httpVersion) : this(
            new Method(method),
			new AsText(url),
			httpVersion
		)
		{ }
    }
}

