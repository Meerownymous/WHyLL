using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// A copy of the rendering message but without
    /// headers that do not match the allow function.
    /// </summary>
	public sealed class WithoutHeaders(
        Func<IPair<string, string>, bool> shouldRemove,
        string firstLine, IEnumerable<IPair<string, string>> parts, Stream body
    ) : IRendering<IMessage>
    {
        /// <summary>
        /// A copy of the rendering message but without
        /// headers that do not match the allow function.
        /// </summary>
        public WithoutHeaders(Func<IPair<string, string>, bool> shouldRemove) : this(
            shouldRemove, string.Empty, None._<IPair<string, string>>(), new MemoryStream()
        )
        { }

        public IRendering<IMessage> Refine(string firstLine) =>
            new WithoutHeaders(shouldRemove, firstLine, parts, body);

        public IRendering<IMessage> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts);

        public IRendering<IMessage> Refine(params IPair<string, string>[] parts) =>
            new WithoutHeaders(shouldRemove, firstLine, Joined._(parts, parts), body);

        public IRendering<IMessage> Refine(Stream body) =>
            new WithoutHeaders(shouldRemove, firstLine, parts, body);

        public async Task<IMessage> Render()
        {
            return
                await Task.FromResult(
                    new SimpleMessage(
                        firstLine,
                        Filtered._(
                            header => !shouldRemove(header),
                            parts
                        ),
                        body
                    )
                );
        }
    }
}