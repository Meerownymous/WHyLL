using WHyLL.Warp;

namespace WHyLL.Warp
{
    public sealed class FromScratch<TOutput> : WarpEnvelope<TOutput>
    {
        /// <summary>
        /// Renders a fixed outcome.
        /// </summary>
        public FromScratch(TOutput output) : base(
            new PiecesAs<TOutput>((_, _, _) => Task.FromResult(output))
        )
        { }

        /// <summary>
        /// Warp from scratch, with no inputs.
        /// </summary>
        public FromScratch(Func<TOutput> render) : base(
            new PiecesAs<TOutput>((_, _, _) => Task.FromResult(render()))
        )
        { }
        
        /// <summary>
        /// Warp from scratch, with no inputs.
        /// </summary>
        public FromScratch(Func<Task<TOutput>> asyncRender) : base(
            new PiecesAs<TOutput>((_,_,_) => asyncRender())
        )
        { }
    }

    public static class FromScratch
    {
        /// <summary>
        /// Rendering from scratch, with no inputs.
        /// </summary>
        public static FromScratch<TOutput> _<TOutput>(Func<TOutput> render) =>
            new(render);

        /// <summary>
        /// Rendering from scratch, with no inputs.
        /// </summary>
        public static FromScratch<TOutput> _<TOutput>(Func<Task<TOutput>> renderAsync) =>
            new(renderAsync);
    }
}

namespace WHyLL
{
    public static class FromScratchSmarts
    {
        /// <summary>
        /// Renders a fixed outcome.
        /// </summary>
        public static Task<TOutput> FromScratch<TOutput>(IMessage message, TOutput output) =>
            message.To(new FromScratch<TOutput>(output));
        
        /// <summary>
        /// Renders a fixed outcome.
        /// </summary>
        public static Task<TOutput> FromScratch<TOutput>(IMessage message, Func<Task<TOutput>> renderAsync) =>
            message.To(new FromScratch<TOutput>(renderAsync));
    }
}