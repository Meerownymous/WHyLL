using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message
{
    /// <summary>
    /// Simple HTTP message.
    /// </summary>
    public sealed class SimpleMessage(
        Func<string> firstLine, 
        IEnumerable<IPair<string,string>> parts, 
        Stream body
    ) : IMessage
    {
        private readonly Lazy<string> firstLine = new(firstLine);

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

        public IMessage With(string firstLine) =>
            new SimpleMessage(() => firstLine, parts, body);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.With(newParts.ToArray());

        public IMessage With(params IPair<string, string>[] newParts)
        {
            return
                new SimpleMessage(
                    this.firstLine.Value,
                    Joined._(parts, newParts),
                    body
                );
        }

        public IMessage WithBody(Stream newBody) =>
            new SimpleMessage(this.firstLine.Value, parts, newBody);

        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            rendering = rendering.Refine(this.firstLine.Value);
            foreach (var part in parts)
                rendering = rendering.Refine(part);
            rendering = rendering.Refine(body);
            return await rendering.Render();
        }
    }
}

