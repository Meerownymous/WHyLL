using System.Net;
using Tonga.Text;

namespace WHyLL.Http.Response
{
	/// <summary>
	/// The response line, first line of a http response.
	/// </summary>
	public sealed class ResponseLine : TextEnvelope
	{
        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponseLine(int statusCode) : this(
			statusCode, new Version(1, 1)
		)
		{ }

        /// <summary>
        /// The response line, first line of a http response.
        /// </summary>
        public ResponseLine(int statusCode, Version httpVersion) : base(
            AsText._(
                $"HTTP/{httpVersion.Major}.{httpVersion.Minor} {statusCode} {Enum.GetName(typeof(HttpStatusCode), statusCode)}"
            )
		)
		{ }
    }
}