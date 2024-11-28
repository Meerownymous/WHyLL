using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Warp
{
    /// <summary>
    /// A copy of the Warp message but without
    /// headers that do not match the allow function.
    /// </summary>
	public sealed class WithoutHeaders(
        Func<IPair<string, string>, bool> shouldRemove,
        string firstLine, IEnumerable<IPair<string, string>> parts, Stream body
    ) : IWarp<IMessage>
    {
        /// <summary>
        /// A copy of the Warp message but without
        /// headers that do not match the allow function.
        /// </summary>
        public WithoutHeaders(Func<IPair<string, string>, bool> shouldRemove) : this(
            shouldRemove, string.Empty, None._<IPair<string, string>>(), new MemoryStream()
        )
        { }

        public IWarp<IMessage> Refine(string newFirstLine) =>
            new WithoutHeaders(shouldRemove, newFirstLine, parts, body);

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            new WithoutHeaders(shouldRemove, firstLine, Joined._(parts, newParts), body);

        public IWarp<IMessage> Refine(params IPair<string, string>[] newParts) =>
            new WithoutHeaders(shouldRemove, firstLine, Joined._(parts, newParts), body);

        public IWarp<IMessage> Refine(Stream newBody) =>
            new WithoutHeaders(shouldRemove, firstLine, parts, newBody);

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