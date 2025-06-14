using Tonga;
using Tonga.Enumerable;
using WHyLL.Prologue;
using WHyLL.Warp;

namespace WHyLL.Warp
{

    /// <summary>
    /// Renders output from pieces of a message.
    /// </summary>
    public sealed class PiecesAs<TOutput>(
        Func<IPrologue, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render,
        IPrologue prologue,
        IEnumerable<IPair<string, string>> parts,
        Stream body
    ) : IWarp<TOutput>
    {
        /// <summary>
        /// Renders output from pieces of a message.
        /// </summary>
        public PiecesAs(
            Func<IPrologue, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render) : this(
            render, 
            new Blank(), 
            new None<IPair<string, string>>(), 
            new MemoryStream()
        )
        {
        }

        public IWarp<TOutput> Refine(IPrologue newPrologue) =>
            new PiecesAs<TOutput>(render, newPrologue, parts, body);

        public IWarp<TOutput> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(newParts.ToArray());

        public IWarp<TOutput> Refine(params IPair<string, string>[] newParts) =>
            new PiecesAs<TOutput>(render, prologue, parts.AsJoined(newParts), body);

        public IWarp<TOutput> Refine(Stream newBody) =>
            new PiecesAs<TOutput>(render, prologue, parts, newBody);

        public async Task<TOutput> Render() =>
            await render(prologue, parts, body);
    }
}

namespace WHyLL
{
    public static class PiecesAsSmarts
    {
        public static Task<TOutput> PiecesAs<TOutput>(
            this IMessage message,
            Func<IPrologue, IEnumerable<IPair<string, string>>, Stream, Task<TOutput>> render
        ) => message.To(new PiecesAs<TOutput>(render));
    }
}