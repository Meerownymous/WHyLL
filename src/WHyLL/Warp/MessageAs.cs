using WHyLL.Message;

namespace WHyLL.Warp
{
    /// <summary>
    /// Desired output from message.
    /// </summary>
    public sealed class MessageAs<TOutput>(Func<IMessage, Task<TOutput>> render) :
        WarpEnvelope<TOutput>(
            new PiecesAs<TOutput>((firstLine, headers, body) =>
                render(
                    new SimpleMessage()
                        .With(firstLine)
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

