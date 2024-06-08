using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from a message.
    /// </summary>
    public sealed class MessageAs<TOutput> : RenderingEnvelope<TOutput>
    {
        /// <summary>
        /// Renders output from a message.
        /// </summary>
        public MessageAs(
            Func<IMessage, Task<TOutput>> render) : base(
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
            new MessageAs<TOutput>(render);
    }
}

