namespace WHyLL.MessageInput
{
    /// <summary>
    /// Body input for a <see cref="IMessage"/>.
    /// </summary>
	public sealed class BodyInput(Func<Stream> body) : IMessageInput
	{
        public BodyInput(Stream body) : this (() => body)
        { }
        
        public IMessage WriteTo(IMessage message) =>
            message.WithBody(body());
    }
}

