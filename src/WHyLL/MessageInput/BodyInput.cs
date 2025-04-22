using Newtonsoft.Json.Linq;
using WHyLL;
using WHyLL.Message;
using WHyLL.MessageInput;

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

namespace BodyInputSmarts
{
    public static class BodyInputSmarts
    {
        public static IMessage Body(this IMessage msg, Stream body) => 
            new MessageWithInputs(
                new BodyInput(body)
            );
    }
}