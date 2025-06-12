using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.Prologue;

namespace WHyLL.Warp
{
    /// <summary>
    /// A copy of the Warp message but without
    /// headers that do not match the allow function.
    /// </summary>
	public sealed class WithoutHeaders(
        Func<IPair<string, string>, bool> shouldRemove,
        IPrologue prologue, IEnumerable<IPair<string, string>> parts, Stream body
    ) : IWarp<IMessage>
    {
        /// <summary>
        /// A copy of the Warp message but without
        /// headers that do not match the allow function.
        /// </summary>
        public WithoutHeaders(Func<IPair<string, string>, bool> shouldRemove) : this(
            shouldRemove, new Blank(), new None<IPair<string, string>>(), new MemoryStream()
        )
        { }

        public IWarp<IMessage> Refine(IPrologue newPrologue) =>
            new WithoutHeaders(shouldRemove, newPrologue, parts, body);

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            new WithoutHeaders(shouldRemove, prologue, parts.AsJoined(newParts), body);

        public IWarp<IMessage> Refine(params IPair<string, string>[] newParts) =>
            new WithoutHeaders(shouldRemove, prologue, parts.AsJoined(newParts), body);

        public IWarp<IMessage> Refine(Stream newBody) =>
            new WithoutHeaders(shouldRemove, prologue, parts, newBody);

        public async Task<IMessage> Render()
        {
            return
                await Task.FromResult(
                    new SimpleMessage(
                        prologue,
                        parts.AsFiltered(header => !shouldRemove(header)),
                        body
                    )
                );
        }
    }
}