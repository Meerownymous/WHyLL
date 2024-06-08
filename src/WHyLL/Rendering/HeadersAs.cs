using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render headers of a message as output type.
    /// </summary>
    public sealed class HeadersAs<TOutput> : RenderingEnvelope<TOutput>
    {
        /// <summary>
        /// Render headers of a message as output type.
        /// </summary>
        public HeadersAs(Func<IEnumerable<IPair<string, string>>, TOutput> render) : this(
            headers => Task.FromResult(render(headers))
        )
        { }

        /// <summary>
        /// Render headers of a message as output type.
        /// </summary>
        public HeadersAs(Func<IEnumerable<IPair<string, string>>, Task<TOutput>> renderAsync) : base(
            new PiecesAs<TOutput>((x,headers,z) => renderAsync(headers))
        )
        { }

    }

    /// <summary>
    /// Render headers of a message as output type.
    /// </summary>
    public static class HeadersAs
    {
        public static HeadersAs<TOutput> _<TOutput>(
            Func<IEnumerable<IPair<string, string>>, Task<TOutput>> renderAsync
        ) => new HeadersAs<TOutput>(renderAsync);
    }
}

