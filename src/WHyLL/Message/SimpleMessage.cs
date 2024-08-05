using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message
{
    /// <summary>
    /// Simple HTTP message.
    /// </summary>
    public sealed class SimpleMessage : IMessage
    {
        private readonly Func<string> firstLine;
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;

        /// <summary>
        /// Simple HTTP message.
        /// </summary>
        public SimpleMessage() : this(
            String.Empty, None._<IPair<string,string>>(), new MemoryStream()
        )
        { }

        /// <summary>
        /// Simple HTTP message.
        /// </summary>
        public SimpleMessage(IText firstLine, IEnumerable<IPair<string, string>> parts, Stream body) : this(
            firstLine.AsString, parts, body
        )
        { }

        /// <summary>
        /// Simple HTTP message.
        /// </summary>
        public SimpleMessage(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body) : this(
            () => firstLine, parts, body
        )
        { }

        /// <summary>
        /// Simple HTTP message.
        /// </summary>
        public SimpleMessage(Func<string> firstLine, IEnumerable<IPair<string,string>> parts, Stream body)
        {
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public IMessage With(string firstLine) =>
            new SimpleMessage(() => firstLine, this.parts, this.body);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.With(newParts.ToArray());

        public IMessage With(params IPair<string, string>[] newParts)
        {
            return
                new SimpleMessage(
                    this.firstLine,
                    Joined._(this.parts, newParts),
                    this.body
                );
        }

        public IMessage WithBody(Stream newBody) =>
            new SimpleMessage(this.firstLine, this.parts, newBody);

        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            rendering = rendering.Refine(this.firstLine());
            foreach (var part in this.parts)
                rendering = rendering.Refine(part);
            rendering = rendering.Refine(body);
            return await rendering.Render();
        }
    }
}

