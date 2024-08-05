using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render the first line of a request (request-line) or response (status-line)
    /// </summary>
	public sealed class FirstLine() : RenderingEnvelope<string>(
	    new FirstLineAs<string>(line => line)
	)
	{ }
}

