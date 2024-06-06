using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// A copy of the rendering message but without
    /// headers that do not match the allow function.
    /// </summary>
	public sealed class WithoutHeaders : IRendering<IMessage>
    {
        private readonly Func<IPair<string, string>, bool> shouldRemove;
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;

        /// <summary>
        /// A copy of the rendering message but without
        /// headers that do not match the allow function.
        /// </summary>
        public WithoutHeaders(Func<IPair<string, string>, bool> shouldRemove) : this(
            shouldRemove, string.Empty, None._<IPair<string, string>>(), new MemoryStream()
        )
        { }

        /// <summary>
        /// A copy of the rendering message but without
        /// headers that do not match the allow function.
        /// </summary>
        private WithoutHeaders(
            Func<IPair<string, string>, bool> shouldRemove,
            string firstLine, IEnumerable<IPair<string, string>> parts, Stream body
        )
        {
            this.shouldRemove = shouldRemove;
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IRendering<IMessage> Refine(string firstLine) =>
            new WithoutHeaders(this.shouldRemove, firstLine, this.parts, this.body);

        public IRendering<IMessage> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts);

        public IRendering<IMessage> Refine(params IPair<string, string>[] parts) =>
            new WithoutHeaders(this.shouldRemove, this.firstLine, Joined._(this.parts, parts), this.body);

        public IRendering<IMessage> Refine(Stream body) =>
            new WithoutHeaders(this.shouldRemove, this.firstLine, this.parts, body);

        public async Task<IMessage> Render()
        {
            return
                await Task.FromResult(
                    new SimpleMessage(
                        this.firstLine,
                        Filtered._(
                            header => !this.shouldRemove(header),
                            this.parts
                        ),
                        this.body
                    )
                );
        }
    }
}