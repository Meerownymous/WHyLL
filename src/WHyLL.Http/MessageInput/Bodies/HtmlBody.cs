using WHyLL.Http.Headers;
using WHyLL.MessageInput;

namespace WHyLL.Http.MessageInput.Bodies;

/// <summary>
/// Html Body message input.
/// </summary>
public sealed class HtmlBody(Stream body) : MessageInputEnvelope(
	new JoinedInput(
		new HeaderInput(
			new ContentType("text/html")
		),
		new BodyInput(body)
	)
);
