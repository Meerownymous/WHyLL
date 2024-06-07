using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public sealed class FromPieces<TOutput> : IRendering<TOutput>
    {
        private readonly Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render;

        private readonly string firstLine;
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;

        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public FromPieces(
            Func<string,IEnumerable<IPair<string,string>>,Stream, Task<TOutput>> render) : this(
            render, string.Empty, None._<IPair<string,string>>(), new MemoryStream()
        )
        { }

        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        private FromPieces(
            Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render,
            string firstLine,
            IEnumerable<IPair<string,string>> headers,
            Stream body
        )
        {
            this.render = render;
            this.firstLine = firstLine;
            this.parts = headers;
            this.body = body;
        }

        public IRendering<TOutput> Refine(string start) =>
            new FromPieces<TOutput>(this.render, start, this.parts, this.body);

        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) =>
            new FromPieces<TOutput>(this.render, this.firstLine, Joined._(this.parts, parts), this.body);

        public IRendering<TOutput> Refine(Stream body) =>
            new FromPieces<TOutput>(this.render, this.firstLine, this.parts, body);

        public async Task<TOutput> Render()
        {
            return await this.render(this.firstLine, this.parts, this.body);
        }
    }

    public static class FromPieces
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public static FromPieces<TOutput> _<TOutput>(
            Func<string, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render
        ) =>
            new FromPieces<TOutput>(render);

    }
}

