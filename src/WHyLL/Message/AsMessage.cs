using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from pieces. 
    /// </summary>
    public sealed class AsMessage : IMessage
    {
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string, string>> parts;
        private readonly Stream body;


        /// <summary>
        /// Message from pieces.
        /// </summary>
        public AsMessage(string firstLine, IEnumerable<IPair<string,string>> parts, Stream body)
        {
            this.firstLine = firstLine;
            this.parts = parts;
            this.body = body;
        }

        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            return await
                rendering.Refine(this.firstLine)
                    .Refine(this.parts)
                    .Refine(this.body)
                    .Render();
        }

        public IMessage With(string firstLine) =>
            new AsMessage(firstLine, this.parts, body);

        public IMessage With(IEnumerable<IPair<string, string>> parts) =>
            this.With(parts.ToArray());

        public IMessage With(params IPair<string, string>[] parts) =>
            new AsMessage(this.firstLine, Joined._(this.parts, parts), this.body);

        public IMessage WithBody(Stream body) =>
            new AsMessage(this.firstLine, this.parts, body);

        /// <summary>
        /// Message from pieces.
        /// </summary>
        public static AsMessage _(string firstLine, IEnumerable<IPair<string,string>> parts, Stream body) =>
            new AsMessage(firstLine, parts, body);
    }
}
