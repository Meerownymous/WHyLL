using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    /// <summary>
    /// A copy of the rendering message.
    /// </summary>
	public sealed class MessageCopy : IRendering<IMessage>
	{
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string,string>> parts;
        private readonly Stream body;

        /// <summary>
        /// A copy of the rendering message.
        /// </summary>
        public MessageCopy() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        private MessageCopy(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body)
        {
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IRendering<IMessage> Refine(string firstLine)
        {
            return new MessageCopy(firstLine, this.parts, this.body);
        }

        public IRendering<IMessage> Refine(IPair<string,string> part)
        {
            return new MessageCopy(this.firstLine, Joined._(this.parts, part), this.body);
        }

        public IRendering<IMessage> Refine(Stream body)
        {
            return new MessageCopy(this.firstLine, this.parts, body);
        }

        public async Task<IMessage> Render()
        {
            return
                await Task.FromResult(
                    new SimpleMessage(this.firstLine, this.parts, this.body)
                );
        }
    }
}

