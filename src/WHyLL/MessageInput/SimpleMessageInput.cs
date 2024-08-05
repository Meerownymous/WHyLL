using Tonga;

namespace WHyLL.MessageInput;
/// <summary>
/// Simple <see cref="IMessageInput"/>
/// </summary>
public sealed class SimpleMessageInput(
    params Func<IMessage, IMessage>[] transformations
) : IMessageInput
{
    public SimpleMessageInput(string firstLine) : this(
        msg => msg.With(firstLine)
    )
    { }
    
    public SimpleMessageInput(string firstLine, IEnumerable<IPair<string,string>> headers) : this(
        msg => msg.With(firstLine),
        msg => msg.With(headers)
    )
    { }
    
    public SimpleMessageInput(string firstLine, IEnumerable<IPair<string,string>> headers, Stream body) : this(
        msg => msg.With(firstLine),
        msg => msg.With(headers),
        msg => msg.WithBody(body)
    )
    { }
    
    public IMessage WriteTo(IMessage message)
    {
        foreach (var tfm in transformations)
        {
            message = tfm.Invoke(message);
        }
        return message;
    }
}

