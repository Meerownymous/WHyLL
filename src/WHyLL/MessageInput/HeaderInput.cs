using Newtonsoft.Json.Linq;
using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using WHyLL;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.MessageInput
{
    /// <summary>
    /// Header input for a <see cref="IMessage"/>.
    /// </summary>
	public sealed class HeaderInput(IEnumerable<IPair<string,string>> headers) : MessageInputEnvelope(
        new SimpleMessageInput(
            msg => msg.With(headers)
        )
    )
	{
        /// <summary>
        /// Header input for a <see cref="IMessage"/>.
        /// </summary>
        public HeaderInput(string header, string value) : this(
            (header, value).AsPair()
        )
        { }
        
        /// <summary>
        /// Header input for a <see cref="IMessage"/>.
        /// </summary>
        public HeaderInput(params IPair<string, string>[] headers) : this(
            headers.AsEnumerable()
        )
        { }
    }
}

public static class HeaderInputSmarts
{
    /// Message with header.
    public static IMessage Header(this IMessage msg, string name, string value) => 
        new MessageWithInputs(
            msg,
            new HeaderInput(name, value)
        );
}