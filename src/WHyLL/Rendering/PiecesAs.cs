using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public sealed class PiecesAs<TOutput>(
        Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render,
        string firstLine,
        IEnumerable<IPair<string,string>> headers,
        Stream body
    ) : IRendering<TOutput>
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public PiecesAs(
            Func<string,IEnumerable<IPair<string,string>>,Stream, Task<TOutput>> render) : this(
            render, string.Empty, None._<IPair<string,string>>(), new MemoryStream()
        )
        { }

        public IRendering<TOutput> Refine(string start) =>
            new PiecesAs<TOutput>(render, start, headers, body);

        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) =>
            new PiecesAs<TOutput>(render, firstLine, Joined._(parts, parts), body);

        public IRendering<TOutput> Refine(Stream body) =>
            new PiecesAs<TOutput>(render, firstLine, headers, body);

        public async Task<TOutput> Render() =>
            await render(firstLine, headers, body);
    }

    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public static class PiecesAs
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public static PiecesAs<TOutput> _<TOutput>(
            Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render
        ) =>
            new(render);

    }
}

