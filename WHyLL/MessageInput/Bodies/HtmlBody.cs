using WHyLL.Headers;
using WHyLL.Message;

namespace WHyLL.MessageInput.Bodies
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
