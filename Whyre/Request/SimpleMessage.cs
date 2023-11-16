using Tonga;
using Tonga.Enumerable;
using Whyre.Parts;

namespace Whyre
{
    /// <summary>
    /// Simple HTTP message.
    /// </summary>
    public sealed class SimpleMessage : IMessage
    {
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;

        public SimpleMessage(string firstLine, IEnumerable<IPair<string,string>> parts, Stream body)
        {
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IMessage With(string firstLine)
        {
            return new SimpleMessage(firstLine, this.parts, this.body);
        }

        public IMessage With(IPair<string, string> part)
        {
            return new SimpleMessage(this.firstLine, Joined._(this.parts, part), this.body);
        }

        public IMessage WithBody(Stream body)
        {
            return new SimpleMessage(this.firstLine, this.parts, body);
        }

        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            rendering = rendering.Refine(this.firstLine);
            foreach (var part in this.parts)
                rendering = rendering.Refine(part);
            rendering = rendering.Refine(body);
            return await rendering.Render();
        }
    }
}

