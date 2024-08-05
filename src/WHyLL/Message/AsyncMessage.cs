using Tonga;
using WHyLL.Rendering;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from async function.
    /// </summary>
    public sealed class AsyncMessage(Func<Task<IMessage>> asyncMessage) : IMessage
    {
        public async Task<T> Render<T>(IRendering<T> rendering) =>
            await asyncMessage().Render(rendering);

        public IMessage With(string firstLine) =>
            asyncMessage().Result.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.With(newParts.ToArray());

        public IMessage With(params IPair<string, string>[] newParts) =>
            asyncMessage().Result.With(newParts);

        public IMessage WithBody(Stream newBody) =>
            asyncMessage().Result.WithBody(newBody);

        public static AsyncMessage _(Func<Task<IMessage>> message) =>
            new AsyncMessage(message);
    }
}