using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders output from body of a message.
    /// </summary>
    public sealed class FromBody<TOutput> : IRendering<TOutput>
    {
        private readonly Func<Stream, Task<TOutput>> render;
        private readonly Stream body;

        /// <summary>
        /// Renders output from body of a message.
        /// </summary>
        public FromBody(
            Func<Stream, Task<TOutput>> render) : this(
            render, new MemoryStream()
        )
        { }

        /// <summary>
        /// Renders output from body of a message.
        /// </summary>
        private FromBody(
            Func<Stream, Task<TOutput>> render,
            Stream body
        )
        {
            this.render = render;
            this.body = body;
        }

        public IRendering<TOutput> Refine(string start) => this;
        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) => this;
        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) => this;

        public IRendering<TOutput> Refine(Stream body) => new FromBody<TOutput>(this.render, body);

        public async Task<TOutput> Render()
        {
            return await this.render(this.body);
        }
    }

    public static class FromBody
    {
        /// <summary>
        /// Renders output from body of a message.
        /// </summary>
        public static FromBody<TOutput> _<TOutput>(
            Func<Stream, Task<TOutput>> render
        ) =>
            new FromBody<TOutput>(render);

    }
}

