using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http3;

/// <summary>
/// HTTP GET Request.
/// </summary>
public sealed class Get(string url, IMessageInput input, params IMessageInput[] more) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("GET", url, new Version(2, 0))
                ),
                new Joined<IMessageInput>(input, more)
            )
        )
    )
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url, params IPair<string, string>[] headers) : this(
        url, new HeaderInput(headers)
    )
    { }
}

