using Tonga.Text;

namespace WHyLL.Http.Request
{
	/// <summary>
	/// Http Method.
	/// </summary>
	public sealed class Method(string method) : TextEnvelope(
		new Upper(
			new Strict(
				method,
				ignoreCase: true,
				"GET", "PUT", "POST", "DELETE", "CONNECT", "OPTIONS", "TRACE", "HEAD"
			)
		)    
    )
	{ }
}

