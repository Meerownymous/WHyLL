using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Warp
{
    /// <summary>
    /// A copy of the Warp message.
    /// </summary>
	public sealed class Clone(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body) : IWarp<IMessage>
	{
        /// <summary>
        /// A copy of the Warp message.
        /// </summary>
        public Clone() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        public IWarp<IMessage> Refine(string newFirstLine) =>
            new Clone(newFirstLine, parts, body);

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(newParts.ToArray());

        public IWarp<IMessage> Refine(params IPair<string,string>[] newParts) =>
            new Clone(firstLine, Joined._(parts, newParts), body);

        public IWarp<IMessage> Refine(Stream newBody) =>
            new Clone(firstLine, parts, newBody);

        public async Task<IMessage> Render()
        {
            var bodyClone = new MemoryStream();
            var pos = body.Position;
            body.Seek(0, SeekOrigin.Begin);
            body.CopyTo(bodyClone);
            body.Seek(pos, SeekOrigin.Begin);
            return
                await Task.FromResult(
                    new SimpleMessage(firstLine, parts, bodyClone)
                );
        }
    }
}

