using Tonga;
using Tonga.Enumerable;

namespace Whyre.Wire
{
	public sealed class Response : IRendering<IMessage>
	{
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string,string>> parts;
        private readonly Stream body;

        public Response() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        public Response(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body)
		{
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IRendering<IMessage> Refine(string firstLine)
        {
            return new Response(firstLine, this.parts, this.body);
        }

        public IRendering<IMessage> Refine(IPair<string,string> part)
        {
            return new Response(this.firstLine, Joined._(this.parts, part), this.body);
        }

        public IRendering<IMessage> Refine(Stream body)
        {
            return new Response(this.firstLine, this.parts, body);
        }

        public async Task<IMessage> Render()
        {
            return
                await
                    Task.Run(() =>
                        new Get(
                            new Uri("http://www.google.de"),
                            AsEnumerable._<IPair<string,string>>()
                        ).WithBody(this.body)
                    );
        }
    }
}

