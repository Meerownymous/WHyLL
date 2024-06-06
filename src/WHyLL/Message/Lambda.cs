using Tonga;

namespace WHyLL.Message
{
    /// <summary>
    /// Message from lambda function. This object is Sticky (remembers the func output).
    /// </summary>
    public sealed class Lambda : IMessage
    {
        private readonly Lazy<IMessage> message;

        /// <summary>
        /// Message from lambda function. This object is Sticky (remembers the func output).
        /// </summary>
        public Lambda(Func<IMessage> message)
        {
            this.message = new Lazy<IMessage>(message);
        }

        public Task<T> Render<T>(IRendering<T> rendering)
        {
            return this.message.Value.Render(rendering);
        }

        public IMessage With(string firstLine)=>
            this.message.Value.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> parts) =>
            this.With(parts.ToArray());

        public IMessage With(params IPair<string, string>[] parts) =>
            this.message.Value.With(parts);

        public IMessage WithBody(Stream body) =>
            this.message.Value.WithBody(body);

        /// <summary>
        /// Message from lambda function. This object is Sticky (remembers the func output).
        /// </summary>
        public static Lambda _(Func<IMessage> source) =>
            new Lambda(source);
    }
}

