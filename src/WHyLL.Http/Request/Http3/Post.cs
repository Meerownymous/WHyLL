using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http3;

/// <summary>
/// HTTP POST Request.
/// </summary>
public sealed class Post(string url, Stream body, IMessageInput input, params IMessageInput[] more) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("POST", url, new Version(3, 0)),
                    body
                ),
                new Joined<IMessageInput>(input, more)
            )
        )
    )
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public Post(string url, Stream body, params IPair<string, string>[] headers) : this(
        url, body, new HeaderInput(headers)
    )
    { }
}