using System.Text;
using Tonga.IO;
using WHyLL;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.MessageInput
{
    /// <summary>
    /// String body input for a <see cref="IMessage"/>.
    /// </summary>
    public sealed class StringBodyInput(Func<string> body) : IMessageInput
    {
        /// <summary>
        /// String body input for a <see cref="IMessage"/>.
        /// </summary>
        public StringBodyInput(string body) : this (() => body)
        { }
        
        public IMessage WriteTo(IMessage message) =>
            message.WithBody(
                new AsInputStream(body(), new UTF8Encoding())    
            );
    }
}

namespace BodyInputSmarts
{
    public static class StringBodyInputSmarts
    {
        /// <summary>
        /// Message with string body.
        /// </summary>
        public static IMessage Body(this IMessage msg, string body) => 
            new MessageWithInputs(msg, new StringBodyInput(body));
    }
}