using System;
using WHyLL.Rendering;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Rendering from scratch, with no inputs.
    /// </summary>
    public sealed class FromScratch<TOutput> : RenderingEnvelope<TOutput>
    {
        /// <summary>
        /// Rendering from scratch, with no inputs.
        /// </summary>
        public FromScratch(Func<TOutput> render) : base(
            new PiecesAs<TOutput>((_, _, _) => Task.FromResult(render()))
        )
        { }

        /// <summary>
        /// Rendering from scratch, with no inputs.
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
