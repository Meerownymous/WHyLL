namespace Whyre.Message
{
    /// <summary>
    /// Envelope for <see cref="IMessageInput"/>
    /// </summary>
	public abstract class MessageInputEnvelope : IMessageInput
	{
        private readonly IMessageInput input;

        /// <summary>
        /// Envelope for <see cref="IMessageInput"/>
        /// </summary>
        public MessageInputEnvelope(IMessageInput input)
		{
            this.input = input;
        }

        public IMessage WriteTo(IMessage message)
        {
            return this.input.WriteTo(message);
        }
    }
}

