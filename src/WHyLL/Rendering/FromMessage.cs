using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Rendering
{
    public sealed class FromMessage<TOutput> : IRendering<TOutput>
    {
        private readonly Func<IMessage, Task<TOutput>> render;

        private readonly string firstLine;
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;

        public FromMessage(
            Func<IMessage, Task<TOutput>> render) : this(
            render, string.Empty, None._<IPair<string,string>>(), new MemoryStream()
        )
        { }

        private FromMessage(
            Func<IMessage, Task<TOutput>> render,
            string firstLine,
            IEnumerable<IPair<string,string>> headers,
            Stream body
        )
        {
            this.render = render;
            this.firstLine = firstLine;
            this.parts = headers;
            this.body = body;
        }

        public IRendering<TOutput> Refine(string start) =>
            new FromMessage<TOutput>(this.render, start, this.parts, this.body);

        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) =>
            new FromMessage<TOutput>(this.render, this.firstLine, Joined._(this.parts, parts), this.body);

        public IRendering<TOutput> Refine(Stream body) =>
            new FromMessage<TOutput>(this.render, this.firstLine, this.parts, body);

        public async Task<TOutput> Render()
        {
            return await
                this.render(
                    new SimpleMessage()
                        .With(this.firstLine)
                        .With(this.parts)
                        .WithBody(this.body)
                );
        }
    }
}

