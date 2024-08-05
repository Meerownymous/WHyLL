using System;
using Tonga;
using Tonga.Enumerable;

namespace WHyLL.Message
{
    /// <summary>
    /// A <see cref="IMessage" built from <see cref="IMessageInput"s/>/>
    /// </summary>
	public sealed class MessageOfInputs : IMessage
	{
        private readonly Func<IMessage> message;

        /// <summary>
        /// A <see cref="IMessage" built from <see cref="IMessageInput"s/>/>
        /// </summary>
        public MessageOfInputs(params IMessageInput[] inputs) : this(
            AsEnumerable._(inputs) 
        )
        { }

        /// <summary>
        /// A <see cref="IMessage" built from <see cref="IMessageInput"s/>/>
        /// </summary>
        public MessageOfInputs(IEnumerable<IMessageInput> inputs)
		{
            this.message = () =>
            {
                IMessage message = new SimpleMessage();
                foreach (var input in inputs)
                    message = input.WriteTo(message);
                return message;
            };
		}

        public Task<T> Render<T>(IRendering<T> rendering)
        {
            return message().Render(rendering);
        }

        public IMessage With(string firstLine) =>
            message().With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            message().With(newParts.ToArray());

        public IMessage With(params IPair<string, string>[] newParts) =>
            message().With(newParts);

        public IMessage WithBody(Stream body)
        {
            return message().WithBody(body);
        }
    }
}

