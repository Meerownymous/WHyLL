using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http3;

/// <summary>
/// HTTP CONNECT Request.
/// </summary>
public sealed class Connect(string url, IMessageInput input, params IMessageInput[] more) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("CONNECT", url, new Version(3,0))
                ),
                new Joined<IMessageInput>(input, more)
            )
        )
    )
{
    /// <summary>
    /// HTTP CONNECT Request.
    /// </summary>
    public Connect(string url, params IPair<string, string>[] headers) : this(
        url, new HeaderInput(headers)
    )
    { }
}

