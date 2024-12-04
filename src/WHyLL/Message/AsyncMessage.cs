using Tonga;
using WHyLL.Warp;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from async function.
    /// </summary>
    public sealed class AsyncMessage(Func<Task<IMessage>> asyncMessage) : IMessage
    {
        public async Task<T> To<T>(IWarp<T> warp) =>
            await asyncMessage().To(warp);

        public IMessage With(string firstLine) =>
            asyncMessage().Result.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            asyncMessage().Result.With(newParts);

        public IMessage WithBody(Stream newBody) =>
            asyncMessage().Result.WithBody(newBody);

        public static AsyncMessage _(Func<Task<IMessage>> message) =>
            new AsyncMessage(message);
    }
}