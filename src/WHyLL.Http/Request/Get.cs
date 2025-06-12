using Tonga;
using Tonga.Enumerable;
using Tonga.Text;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request;

/// <summary>
/// HTTP GET Request.
/// </summary>
public sealed class Get(Func<string> url, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
    MessageEnvelope(
        new MessageWithInputs(
            new Joined<IMessageInput>(
                inputs,
                new SimpleMessageInput(
                    new RequestPrologue(new AsText("GET"), new AsText(url()), httpVersion)
                )
            )
        )
    )
{
    public Get(string url, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
        this(() => url, httpVersion, inputs)
    { }
    
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url, Version httpVersion, IMessageInput input, params IMessageInput[] inputs) :
        this(url, httpVersion, inputs.AsJoined(input))
    { }
    
    
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url, IPair<string,string> header, params IPair<string, string>[] headers) : this(
        url, new Version(1,1), new HeaderInput(headers.AsJoined(header))
    )
    { }

    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url, Version httpVersion, IPair<string,string> header, params IPair<string, string>[] headers) : this(
        url, httpVersion, new HeaderInput(headers.AsJoined(header))
    )
    { }
    
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url, Version httpVersion) : this(
        url, httpVersion, []
    )
    { }
    
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url) : this(
        url, new Version(1,1), []
    )
    { }

    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public Get(string url, IMessageInput input, params IMessageInput[] more) : this(
        url, new Version(1,1), input, more
    )
    { }
}

