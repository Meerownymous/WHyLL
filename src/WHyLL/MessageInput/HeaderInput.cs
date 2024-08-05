using Tonga;
using Tonga.Enumerable;

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
        public HeaderInput(params IPair<string, string>[] headers) : this(
            AsEnumerable._(headers)
        )
        { }
    }
}

