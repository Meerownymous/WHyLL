using Tonga;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from lambda function. This object is Sticky (remembers the func output).
    /// </summary>
    public sealed class Lambda(Func<IMessage> message) : IMessage
    {
        private readonly Lazy<IMessage> message = new(message);

        public Task<T> To<T>(IWarp<T> warp) =>
            message.Value.To(warp);

        public IMessage With(string firstLine) =>
            message.Value.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.message.Value.With(newParts);

        public IMessage WithBody(Stream newBody) =>
            this.message.Value.WithBody(newBody);

        /// <summary>
        /// Message from lambda function. This object is Sticky (remembers the func output).
        /// </summary>
        public static Lambda _(Func<IMessage> source) =>
            new(source);
    }
}

