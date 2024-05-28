namespace WHyLL.MessageInput
{
    /// <summary>
    /// Body input for a <see cref="IMessage"/>.
    /// </summary>
	public sealed class BodyInput : IMessageInput
	{
        private readonly Stream body;

        /// <summary>
        /// Body input for a <see cref="IMessage"/>.
        /// </summary>
        public BodyInput(Stream body)
		{
            this.body = body;
        }

        public IMessage WriteTo(IMessage message)
        {
            return message.WithBody(this.body);
        }
    }
}

