using Tonga;
using WHyLL.Rendering;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from async function.
    /// </summary>
    public sealed class AsyncMessage : IMessage
    {
        private readonly Func<Task<IMessage>> asyncMessage;

        /// <summary>
        /// Message from async function.
        /// </summary>
        public AsyncMessage(
            Func<Task<IMessage>> asyncMessage
        )
        {
            this.asyncMessage = asyncMessage;
        }

        public async Task<T> Render<T>(IRendering<T> rendering) =>
            await this.asyncMessage().Render(rendering);

        public IMessage With(string firstLine) =>
            this.asyncMessage().Result.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> parts) =>
            this.With(parts.ToArray());

        public IMessage With(params IPair<string, string>[] header) =>
            this.asyncMessage().Result.With(header);

        public IMessage WithBody(Stream body) =>
            this.asyncMessage().Result.WithBody(body);

        public static AsyncMessage _(Func<Task<IMessage>> message) =>
            new AsyncMessage(message);
    }
}