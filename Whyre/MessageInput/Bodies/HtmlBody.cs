using Whyre.Headers;
using Whyre.Message;

namespace Whyre.MessageInput.Bodies
{
	/// <summary>
	/// Html Body message input.
	/// </summary>
	public sealed class HtmlBody : MessageInputEnvelope
	{
        /// <summary>
        /// Html Body message input.
        /// </summary>
        public HtmlBody(Stream body) : base(
			new JoinedInput(
				new HeaderInput(
					new ContentType("text/html")
				),
				new BodyInput(body)
			)
		)
		{ }
	}
}
