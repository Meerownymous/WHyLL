using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from headers of a message.
    /// </summary>
    public sealed class FromHeaders<TOutput> : IRendering<TOutput>
    {
        private readonly Func<IEnumerable<IPair<string, string>>, Task<TOutput>> render;
        private readonly IEnumerable<IPair<string, string>> parts;

        /// <summary>
        /// Renders output from headers of a message.
        /// </summary>
        public FromHeaders(
            Func<IEnumerable<IPair<string,string>>, Task<TOutput>> render) : this(
            render, None._<IPair<string,string>>()
        )
        { }

        /// <summary>
        /// Renders output from headers of a message.
        /// </summary>
        private FromHeaders(
            Func<IEnumerable<IPair<string, string>>, Task<TOutput>> render,
            IEnumerable<IPair<string,string>> headers
        )
        {
            this.render = render;
            this.parts = headers;
        }

        public IRendering<TOutput> Refine(string start) => this;
        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) =>
            new FromHeaders<TOutput>(this.render, Joined._(this.parts, parts));

        public IRendering<TOutput> Refine(Stream body) => this;

        public async Task<TOutput> Render()
        {
            return await this.render(this.parts);
        }
    }

    public static class FromHeaders
    {
        /// <summary>
        /// Renders output from headers of a message.
        /// </summary>
        public static FromHeaders<TOutput> _<TOutput>(
            Func<IEnumerable<IPair<string, string>>, Task<TOutput>> render
        ) =>
            new FromHeaders<TOutput>(render);

    }
}

