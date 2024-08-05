using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message
{
    /// <summary>
    /// A <see cref="IMessage"> built from <see cref="IMessageInput"s/>/>
    /// </summary>
	public sealed class MessageOfInputs(IEnumerable<IMessageInput> inputs) : IMessage
    {
        private readonly Lazy<IMessage> message = new(() =>
        {
            IMessage result = new SimpleMessage();
            foreach (var input in inputs)
                result = input.WriteTo(result);
            return result;
        });

        /// <summary>
        /// A <see cref="IMessage" built from <see cref="IMessageInput"s/>/>
        /// </summary>
        public MessageOfInputs(params IMessageInput[] inputs) : this(
            AsEnumerable._(inputs) 
        )
        { }

        public Task<T> Render<T>(IRendering<T> rendering) =>
            this.message.Value.Render(rendering);

        public IMessage With(string firstLine) =>
            this.message.Value.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.message.Value.With(newParts.ToArray());

        public IMessage With(params IPair<string, string>[] newParts) =>
            this.message.Value.With(newParts);

        public IMessage WithBody(Stream body) =>
            this.message.Value.WithBody(body);
    }
}

