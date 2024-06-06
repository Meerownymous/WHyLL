using Tonga;
using WHyLL;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render the first line of a message as output type.
    /// </summary>
    public sealed class FirstLineAs<Output> : IRendering<Output>
    {
        private readonly Func<string, Output> render;
        private readonly string firstLine;

        /// <summary>
        /// Render the first line of a message as output type.
        /// </summary>
        public FirstLineAs(Func<string, Output> render) : this(
            render, string.Empty
        )
        { }

        /// <summary>
        /// Render the first line of a message as output type.
        /// </summary>
        private FirstLineAs(
            Func<string, Output> render,
            string firstLine
        )
        {
            this.render = render;
            this.firstLine = firstLine;
        }

        public IRendering<Output> Refine(string firstLine) =>
            new FirstLineAs<Output>(
                this.render,
                firstLine
            );

        public IRendering<Output> Refine(params IPair<string, string>[] parts) => this;
        public IRendering<Output> Refine(IEnumerable<IPair<string, string>> parts) => this;
        public IRendering<Output> Refine(Stream body) => this;

        public Task<Output> Render() =>
            Task.Run(() => this.render(this.firstLine));
    }
}

