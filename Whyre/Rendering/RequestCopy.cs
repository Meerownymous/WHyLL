using Tonga;
using Tonga.Enumerable;
using Whyre.Message;

namespace Whyre.Rendering
{
	public sealed class RequestCopy : IRendering<IMessage>
	{
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string,string>> parts;
        private readonly Stream body;

        public RequestCopy() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        public RequestCopy(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body)
		{
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IRendering<IMessage> Refine(string firstLine)
        {
            return new RequestCopy(firstLine, this.parts, this.body);
        }

        public IRendering<IMessage> Refine(IPair<string,string> part)
        {
            return new RequestCopy(this.firstLine, Joined._(this.parts, part), this.body);
        }

        public IRendering<IMessage> Refine(Stream body)
        {
            return new RequestCopy(this.firstLine, this.parts, body);
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

