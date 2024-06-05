using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render headers of a message as output type.
    /// </summary>
    public sealed class HeadersAs<Output> : IRendering<Output>
    {
        private readonly Func<IEnumerable<IPair<string, string>>, Output> render;
        private readonly IEnumerable<IPair<string, string>> parts;

        /// <summary>
        /// Render headers of a message as output type.
        /// </summary>
        public HeadersAs(Func<IEnumerable<IPair<string, string>>, Output> render) : this(
            render, None._<IPair<string, string>>()
        )
        { }

        /// <summary>
        /// Render headers of a message as output type.
        /// </summary>
        private HeadersAs(
            Func<IEnumerable<IPair<string, string>>, Output> render,
            IEnumerable<IPair<string, string>> parts
        )
        {
            this.render = render;
            this.parts = parts;
        }

        public IRendering<Output> Refine(string firstLine) => this;

        public IRendering<Output> Refine(IPair<string, string> header) =>
            new HeadersAs<Output>(
                this.render,
                Joined._(this.parts, header)
            );

        public IRendering<Output> Refine(Stream body) =>
            new HeadersAs<Output>(
                this.render,
                this.parts
            );

        public Task<Output> Render() =>
            Task.Run(() => this.render(this.parts));
    }
}

