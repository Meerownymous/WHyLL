using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render the body of a message as output type.
    /// </summary>
    public sealed class BodyAs<Output> : IRendering<Output>
    {
        private readonly Func<Stream, Output> render;
        private readonly Stream body;

        /// <summary>
        /// Render the body of a message as output type.
        /// </summary>
        public BodyAs(Func<Stream, Output> render) : this(render, new MemoryStream())
        { }

        /// <summary>
        /// Render the body of a message as output type.
        /// </summary>
        private BodyAs(
            Func<Stream, Output> render,
            Stream body
        )
        {
            this.render = render;
            this.body = body;
        }

        public IRendering<Output> Refine(string firstLine) => this;

        public IRendering<Output> Refine(IEnumerable<IPair<string, string>> parts) => this;
        public IRendering<Output> Refine(IPair<string, string>[] parts) => this;

        public IRendering<Output> Refine(Stream body) =>
            new BodyAs<Output>(
                this.render,
                body
            );

        public Task<Output> Render() =>
            Task.Run(() => this.render(this.body));
    }
}

