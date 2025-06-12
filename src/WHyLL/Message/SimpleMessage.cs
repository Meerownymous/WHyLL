using Tonga;
using Tonga.Enumerable;
using WHyLL.Prologue;

namespace WHyLL.Message
{
    /// <summary>
    /// Simple HTTP message.
    /// </summary>
    public sealed class SimpleMessage(
        IPrologue prologue, 
        IEnumerable<IPair<string,string>> parts, 
        Stream body
    ) : IMessage
    {
        /// <summary>
        /// Simple HTTP message.
        /// </summary>
        public SimpleMessage() : this(
            new Blank(), new None<IPair<string,string>>(), new MemoryStream()
        )
        { }

        public IMessage With(IPrologue newPrologue) =>
            new SimpleMessage(newPrologue, parts, body);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            new SimpleMessage(
                prologue,
                parts.AsJoined(newParts),
                body
            );

        public IMessage WithBody(Stream newBody) =>
            new SimpleMessage(prologue, parts, newBody);

        public async Task<T> To<T>(IWarp<T> warp)
        {
            warp = warp.Refine(prologue);
            foreach (var part in parts)
                warp = warp.Refine(part);
            warp = warp.Refine(body);
            return await warp.Render();
        }
    }
}