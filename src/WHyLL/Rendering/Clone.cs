using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// A copy of the rendering message.
    /// </summary>
	public sealed class Clone : IRendering<IMessage>
	{
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string,string>> parts;
        private readonly Stream body;

        /// <summary>
        /// A copy of the rendering message.
        /// </summary>
        public Clone() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        private Clone(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body)
        {
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IRendering<IMessage> Refine(string firstLine) =>
            new Clone(firstLine, this.parts, this.body);

        public IRendering<IMessage> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<IMessage> Refine(params IPair<string,string>[] parts) =>
            new Clone(this.firstLine, Joined._(this.parts, parts), this.body);

        public IRendering<IMessage> Refine(Stream body) =>
            new Clone(this.firstLine, this.parts, body);

        public async Task<IMessage> Render()
        {
            var bodyClone = new MemoryStream();
            var pos = this.body.Position;
            this.body.Seek(0, SeekOrigin.Begin);
            this.body.CopyTo(bodyClone);
            this.body.Seek(pos, SeekOrigin.Begin);
            return
                await Task.FromResult(
                    new SimpleMessage(this.firstLine, this.parts, bodyClone)
                );
        }
    }
}

