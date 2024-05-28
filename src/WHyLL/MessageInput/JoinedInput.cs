
namespace WHyLL.MessageInput
{
    /// <summary>
    /// Multiple <see cref="IMessageInput" joined together. />
    /// </summary>
	public sealed class JoinedInput : IMessageInput
	{
        private readonly IMessageInput[] inputs;

        /// <summary>
        /// Multiple <see cref="IMessageInput" joined together. />
        /// </summary>
        public JoinedInput(params IMessageInput[] inputs)
		{
            this.inputs = inputs;
        }

        public IMessage WriteTo(IMessage message)
        {
            foreach(var input in this.inputs)
            {
                message = input.WriteTo(message);
            }
            return message;
        }
    }
}

