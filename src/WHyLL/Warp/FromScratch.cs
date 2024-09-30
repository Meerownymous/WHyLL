using System;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Warp from scratch, with no inputs.
    /// </summary>
    public sealed class FromScratch<TOutput> : WarpEnvelope<TOutput>
    {
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
        /// Warp from scratch, with no inputs.
        /// </summary>
        public static FromScratch<TOutput> _<TOutput>(Func<TOutput> render) =>
            new(render);

        /// <summary>
        /// Warp from scratch, with no inputs.
        /// </summary>
        public static FromScratch<TOutput> _<TOutput>(Func<Task<TOutput>> renderAsync) =>
            new(renderAsync);
    }
}
