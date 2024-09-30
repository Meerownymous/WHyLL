namespace WHyLL.Warp
{
    /// <summary>
    /// Renders a fixed outcome.
    /// </summary>
    public sealed class Fixed<TOutput>(TOutput output) : WarpEnvelope<TOutput>(
        new PiecesAs<TOutput>((_,_,_) => Task.FromResult(output))
    )
    { }

    public static class Fixed
    {
        /// <summary>
        /// Renders a fixed outcome.
        /// </summary>
        public static Fixed<TOutput> _<TOutput>(TOutput output) =>
            new(output);
    }
}

