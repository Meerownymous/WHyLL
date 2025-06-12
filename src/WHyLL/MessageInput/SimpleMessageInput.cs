using Tonga;
using Tonga.Enumerable;

namespace WHyLL.MessageInput;
/// <summary>
/// Simple <see cref="IMessageInput"/>
/// </summary>
public sealed class SimpleMessageInput(
    params Func<IMessage, IMessage>[] transformations
) : IMessageInput
{
    public SimpleMessageInput(IPrologue prologue) : this(
        msg => msg.With(prologue)
    )
    { }
    
    public SimpleMessageInput(IPrologue prologue, IEnumerable<IPair<string,string>> headers) : this(
        msg => msg.With(prologue),
        msg => msg.With(headers)
    )
    { }
    
    public SimpleMessageInput(IPrologue prologue, Stream body) : this(
        msg => msg.With(prologue),
        msg => msg.WithBody(body)
    )
    { }
    
    public SimpleMessageInput(IPrologue prologue, IEnumerable<IPair<string,string>> headers, Stream body) : this(
        msg => msg.With(prologue),
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

