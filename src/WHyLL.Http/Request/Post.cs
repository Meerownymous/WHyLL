using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request;

/// <summary>
/// HTTP POST Request.
/// </summary>
public sealed class Post(string url, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                new SimpleMessageInput(
                    new RequestPrologue("POST", url, httpVersion)
                ),
                inputs
            )
        )
    )
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public Post(string url, Stream body, IPair<string,string> header, params IPair<string, string>[] headers) : this(
        url, new Version(1,1), body, headers.AsJoined(header)
    )
    { }

    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public Post(string url, Version httpVersion, Stream body, IEnumerable<IPair<string, string>> headers) : this(
        url, 
        httpVersion,
        new JoinedInput(
            new BodyInput(body),
            new HeaderInput(headers)
        ).AsSingle()
    )
    { }

    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public Post(string url, Stream body, IMessageInput input, params IMessageInput[] more) : this(
        url, 
        new Version(1,1), 
        new BodyInput(body)
            .AsSingle()
            .AsJoined(more.AsJoined(input))
    )
    { }
    
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public Post(string url, Stream body) : this(
        url, 
        new Version(1,1), 
        new BodyInput(body).AsSingle()
    )
    { }
    
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public Post(string url, IMessageInput input, params IMessageInput[] more) : this(
        url, 
        new Version(1,1), 
        more.AsEnumerable()
            .AsJoined(input)
    )
    { }
}