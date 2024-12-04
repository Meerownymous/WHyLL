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

        public Task<T> To<T>(IWarp<T> warp) =>
            this.message.Value.To(warp);

        public IMessage With(string firstLine) =>
            this.message.Value.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            this.message.Value.With(newParts.ToArray());

        public IMessage WithBody(Stream body) =>
            this.message.Value.WithBody(body);
    }
}

