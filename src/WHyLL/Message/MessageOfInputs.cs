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

        public IMessage With(string firstLine)
        {
            return message().With(firstLine);
        }

        public IMessage With(IPair<string, string> header)
        {
            return message().With(header);
        }

        public IMessage WithBody(Stream body)
        {
            return message().WithBody(body);
        }
    }
}

