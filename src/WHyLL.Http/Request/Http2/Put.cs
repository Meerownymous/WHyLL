using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http2;

/// <summary>
/// HTTP PUT Request.
/// </summary>
public sealed class Put(string url, Stream body, IMessageInput input, params IMessageInput[] more) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("PUT", url, new Version(2, 0)),
                    body
                ),
                new Joined<IMessageInput>(input, more)
            )
        )
    )
{
    /// <summary>
    /// HTTP PUT Request.
    /// </summary>
    public Put(string url, Stream body, params IPair<string, string>[] headers) : this(
        url, body, new HeaderInput(headers)
    )
    { }
}