using Tonga;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Render headers of a message as output type.
    /// </summary>
    public sealed class HeadersAs<TOutput>(Func<IEnumerable<IPair<string, string>>, Task<TOutput>> renderAsync) : 
        WarpEnvelope<TOutput>(
            new PiecesAs<TOutput>((_, headers, _) => renderAsync(headers))
        )
    {
        /// <summary>
        /// Render headers of a message as output type.
        /// </summary>
        public HeadersAs(Func<IEnumerable<IPair<string, string>>, TOutput> render) : this(
            headers => Task.FromResult(render(headers))
        )
        { }

    }
}

namespace WHyLL
{
    public static class HeadersAsSmarts
    {
        public static Task<TOutput> HeadersAs<TOutput>(
            IMessage message, 
            Func<IEnumerable<IPair<string, string>>, Task<TOutput>> renderAsync
        ) =>
            message.To(new HeadersAs<TOutput>(renderAsync));
    }
}

