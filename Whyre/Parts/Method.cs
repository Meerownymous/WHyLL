using Tonga.Text;

namespace Whyre.Parts
{
	/// <summary>
	/// Http Method.
	/// </summary>
	public sealed class Method : TextEnvelope
	{
        /// <summary>
        /// Http Method.
        /// </summary>
        public Method(string method) : base(
            new Strict(
                method,
                ignoreCase: true,
                "GET", "PUT", "POST", "DELETE", "CONNECT", "OPTIONS", "TRACE", "HEAD"
            )
        )
        { }
    }
}

