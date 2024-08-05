using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from a message.
    /// </summary>
    public sealed class MessageAs<TOutput>(Func<IMessage, Task<TOutput>> render) : 
        RenderingEnvelope<TOutput>(
        new PiecesAs<TOutput>((firstLine, headers, body) =>
                render(
                    new SimpleMessage()
                        .With(firstLine)
                        .With(headers)
                        .WithBody(body)
                )
            )
        )
    { }

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

