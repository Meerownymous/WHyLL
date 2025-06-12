using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http3;

/// <summary>
/// HTTP HEAD Request.
/// </summary>
public sealed class Head(string url, IMessageInput input, params IMessageInput[] more) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("HEAD", url, new Version(3, 0))
                ),
                new Joined<IMessageInput>(input, more)
            )
        )
    )
{
    /// <summary>
    /// HTTP HEAD Request.
    /// </summary>
    public Head(string url, params IPair<string, string>[] headers) : this(
        url, new HeaderInput(headers)
    )
    { }
}

