using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from pieces. 
    /// </summary>
    public sealed class AsMessage(string firstLine, IEnumerable<IPair<string,string>> parts, Stream body) : IMessage
    {
        public async Task<T> Render<T>(IRendering<T> rendering)
        {
            return await
                rendering.Refine(firstLine)
                    .Refine(parts)
                    .Refine(body)
                    .Render();
        }

        public IMessage With(string firstLine) =>
            new AsMessage(firstLine, parts, body);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.With(newParts.ToArray());

        public IMessage With(params IPair<string, string>[] newParts) =>
            new AsMessage(firstLine, Joined._(parts, newParts), body);

        public IMessage WithBody(Stream newBody) =>
            new AsMessage(firstLine, parts, newBody);

        /// <summary>
        /// Message from pieces.
        /// </summary>
        public static AsMessage _(string firstLine, IEnumerable<IPair<string,string>> parts, Stream body) =>
            new(firstLine, parts, body);
    }
}
