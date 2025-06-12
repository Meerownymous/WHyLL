using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request;

/// <summary>
/// HTTP OPTIONS Request.
/// </summary>
public sealed class Options(string url, Version httpVersion, IMessageInput input, params IMessageInput[] more) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("OPTIONS", url, httpVersion)
                ),
                new Joined<IMessageInput>(input, more)
            )
        )
    )
{
    /// <summary>
    /// HTTP OPTIONS Request.
    /// </summary>
    public Options(string url, params IPair<string, string>[] headers) : this(
        url, new Version(1,1), headers
    )
    { }

    /// <summary>
    /// HTTP OPTIONS Request.
    /// </summary>
    public Options(string url, Version httpVersion, params IPair<string, string>[] headers) : this(
        url, httpVersion, new HeaderInput(headers)
    )
    { }

    /// <summary>
    /// HTTP OPTIONS Request.
    /// </summary>
    public Options(string url, IMessageInput input, params IMessageInput[] more) : this(
        url, new Version(1,1), input, more
    )
    { }
}

