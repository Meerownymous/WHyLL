using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;

namespace Whyre
{
    /// <summary>
    /// Simple HTTP Request.
    /// </summary>
    public sealed class SimpleMessage : IMessage
    {
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;

        public SimpleMessage(IEnumerable<IPair<string,string>> parts, Stream body)
        {
            this.parts = parts;
            this.body = body;
        }

        public IMessage Refined(IPair<string, string> part)
        {
            return new SimpleMessage(Joined._(parts, part), this.body);
        }

        public IMessage Refine(Stream body)
        {
            return new SimpleMessage(this.parts, body);
        }

        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            IMessage message = this;
            foreach (var part in this.parts)
                rendering = rendering.Refine(part);
            rendering = rendering.Refine(body);
            return await rendering.Render();
        }
    }
}

