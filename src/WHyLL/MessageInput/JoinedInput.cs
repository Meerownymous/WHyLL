
namespace WHyLL.MessageInput
{
    /// <summary>
    /// Multiple <see cref="IMessageInput" joined together. />
    /// </summary>
	public sealed class JoinedInput(params IMessageInput[] inputs) : IMessageInput
	{
        public IMessage WriteTo(IMessage message)
        {
            foreach(var input in inputs)
            {
                message = input.WriteTo(message);
            }
            return message;
        }
    }
}

