using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// A copy of the rendering message.
    /// </summary>
	public sealed class Clone(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body) : IRendering<IMessage>
	{
        /// <summary>
        /// A copy of the rendering message.
        /// </summary>
        public Clone() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        public IRendering<IMessage> Refine(string firstLine) =>
            new Clone(firstLine, parts, body);

        public IRendering<IMessage> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<IMessage> Refine(params IPair<string,string>[] parts) =>
            new Clone(firstLine, Joined._(parts, parts), body);

        public IRendering<IMessage> Refine(Stream body) =>
            new Clone(firstLine, parts, body);

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

