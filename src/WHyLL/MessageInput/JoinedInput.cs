
using Tonga.Enumerable;

namespace WHyLL.MessageInput
{
    /// <summary>
    /// Multiple <see cref="IMessageInput" joined together. />
    /// </summary>
	public sealed class JoinedInput(IEnumerable<IMessageInput> inputs) : IMessageInput
	{
        public JoinedInput(params IMessageInput[] inputs) : this(AsEnumerable._(inputs))
        { }
        
        public JoinedInput(IEnumerable<IMessageInput> inputs, params IMessageInput[] additional) : this(
            Joined._(
                inputs,
                additional
            )
        )
        { }
        
        public JoinedInput(params IEnumerable<IMessageInput>[] inputs) : this(
            Joined._(
                inputs
            )
        )
        { }
        
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

