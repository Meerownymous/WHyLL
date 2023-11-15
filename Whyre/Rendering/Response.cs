using Tonga;
using Tonga.Enumerable;

namespace Whyre.Wire
{
	public sealed class Response : IRendering<IMessage>
	{
        private readonly IEnumerable<IPair<string,string>> parts;
        private readonly Stream body;

        public Response() : this(None._<IPair<string, string>>(), new MemoryStream())
        { }

        public Response(IEnumerable<IPair<string, string>> parts, Stream body)
		{
            this.parts = parts;
            this.body = body;
        }

        public IRendering<IMessage> Refine(IPair<string,string> part)
        {
            return new Response(Joined._(this.parts, part), this.body);
        }

        public IRendering<IMessage> Refine(Stream body)
        {
            return new Response(this.parts, body);
        }

        public async Task<IMessage> Render()
        {
            return
                await
                    Task.Run(() =>
                        new Get(
                            new Uri("http://www.google.de"),
                            AsEnumerable._<IPair<string,string>>())
                    );
        }
    }
}

