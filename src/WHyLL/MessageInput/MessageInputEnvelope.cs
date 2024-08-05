namespace WHyLL.MessageInput
{
    /// <summary>
    /// Envelope for <see cref="IMessageInput"/>
    /// </summary>
	public abstract class MessageInputEnvelope(IMessageInput input) : IMessageInput
	{
        public IMessage WriteTo(IMessage message) =>
            input.WriteTo(message);
    }
}

