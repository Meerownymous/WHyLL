using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Render the first line of a message as output type.
    /// </summary>
    public sealed class FirstLineAs<Output> : WarpEnvelope<Output>
    {
        /// <summary>
        /// Render the first line of a message as output type.
        /// </summary>
        public FirstLineAs(Func<string, Output> render) : base(
            new PiecesAs<Output>((firstLine, _, _) => Task.FromResult(render(firstLine)))
        )
        { }

        /// <summary>
        /// Render the first line of a message as output type.
        /// </summary>
        public FirstLineAs(Func<string, Task<Output>> render) : base(
            new PiecesAs<Output>((firstLine, _, _) => render(firstLine))
        )
        { }
    }
}

namespace WHyLL
{
    public static class FirstLineAsSmarts
    {
        /// <summary>
        /// Firstline of the message.
        /// </summary>
        public static async Task<TOutput> FirstLineAs<TOutput>(
            this IMessage message, 
            Func<string, TOutput> render
        ) => 
            await message.To(new FirstLineAs<TOutput>(render));
    }
}
