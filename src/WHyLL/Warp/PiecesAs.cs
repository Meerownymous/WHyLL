using Tonga;
using Tonga.Enumerable;
using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public sealed class PiecesAs<TOutput>(
        Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render,
        string firstLine,
        IEnumerable<IPair<string, string>> parts,
        Stream body
    ) : IWarp<TOutput>
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public PiecesAs(
            Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render) : this(
            render, string.Empty, None._<IPair<string, string>>(), new MemoryStream()
        )
        {
        }

        public IWarp<TOutput> Refine(string newFirstLine) =>
            new PiecesAs<TOutput>(render, newFirstLine, parts, body);

        public IWarp<TOutput> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(newParts.ToArray());

        public IWarp<TOutput> Refine(params IPair<string, string>[] newParts) =>
            new PiecesAs<TOutput>(render, firstLine, Joined._(parts, newParts), body);

        public IWarp<TOutput> Refine(Stream newBody) =>
            new PiecesAs<TOutput>(render, firstLine, parts, newBody);

        public async Task<TOutput> Render() =>
            await render(firstLine, parts, body);
    }
}

namespace WHyLL
{
    public static class PiecesAsSmarts
    {
        public static Task<TOutput> PiecesAs<TOutput>(
            this IMessage message,
            Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render
        ) => message.To(new PiecesAs<TOutput>(render));
    }
}