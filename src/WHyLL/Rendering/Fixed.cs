namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders a fixed outcome.
    /// </summary>
    public sealed class Fixed<TOutput> : RenderingEnvelope<TOutput>
    {
        /// <summary>
        /// Renders a fixed outcome.
        /// </summary>
        public Fixed(TOutput output) : base(
            new PiecesAs<TOutput>((x,y,z) => Task.FromResult(output))
        )
        { }
    }

    public static class Fixed
    {
        /// <summary>
        /// Renders a fixed outcome.
        /// </summary>
        public static Fixed<TOutput> _<TOutput>(TOutput output) =>
            new Fixed<TOutput>(output);
    }
}

