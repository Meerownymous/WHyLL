using WHyLL;
using WHyLL.Message;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Desired output from message.
    /// </summary>
    public sealed class MessageAs<TOutput>(Func<IMessage, Task<TOutput>> render) :
        WarpEnvelope<TOutput>(
            new PiecesAs<TOutput>((prologue, headers, body) =>
                render(
                    new SimpleMessage()
                        .With(prologue)
                        .With(headers)
                        .WithBody(body)
                )
            )
        )
    {
        /// <summary>
        /// Desired output from message.
        /// </summary>
        public MessageAs(Func<IMessage, TOutput> render) : this(
            message => Task.FromResult(render(message))
        )
        { }
    }

    /// <summary>
    /// Renders output from a message.
    /// </summary>
    public static class MessageAs
    {
        /// <summary>
        /// Renders output from a message.
        /// </summary>
        public static MessageAs<TOutput> _<TOutput>(
            Func<IMessage, Task<TOutput>> render
        ) =>
            new(render);
    }
}

public static class MessageAsSmarts
{
    public static Task<TOutput> As<TOutput>(this IMessage message, Func<IMessage, Task<TOutput>> render) => 
        message.To(new MessageAs<TOutput>(render));
    
    public static Task<TOutput> As<TOutput>(this IMessage message, Func<IMessage, TOutput> render) => 
        message.To(new MessageAs<TOutput>(render));
}