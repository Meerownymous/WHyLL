using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message;

/// <summary>
/// A <see cref="IMessage"> built from <see cref="IMessageInput"s/>/>
/// </summary>
public sealed class MessageWithInputs(IMessage message, IEnumerable<IMessageInput> inputs) : IMessage
{
    /// <summary>
    /// A <see cref="IMessage"> built from <see cref="IMessageInput"s/>/>
    /// </summary>
    public MessageWithInputs(IEnumerable<IMessageInput> inputs) : this(new SimpleMessage(), inputs)
    { }
    
    private readonly Lazy<IMessage> message = new(() =>
    {
        foreach (var input in inputs)
            message = input.WriteTo(message );
        return message;
    });

    /// <summary>
    /// A <see cref="IMessage" built from <see cref="IMessageInput"s/>/>
    /// </summary>
    public MessageWithInputs(params IMessageInput[] inputs) : this(
        inputs.AsEnumerable()
    )
    { }
    
    /// <summary>
    /// A <see cref="IMessage" built from <see cref="IMessageInput"s/>/>
    /// </summary>
    public MessageWithInputs(IMessage message, params IMessageInput[] inputs) : this(
        message, inputs.AsEnumerable()
    )
    { }

    public Task<T> To<T>(IWarp<T> warp) =>
        this.message.Value.To(warp);

    public IMessage With(IPrologue newPrologue) =>
        this.message.Value.With(newPrologue);

    public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
        this.message.Value.With(newParts.ToArray());

    public IMessage WithBody(Stream body) =>
        this.message.Value.WithBody(body);
}