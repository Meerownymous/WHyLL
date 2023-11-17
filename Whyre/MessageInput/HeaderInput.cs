using Tonga;
using Tonga.Enumerable;

namespace Whyre.MessageInput
{
    /// <summary>
    /// Header input for a <see cref="IMessage"/>.
    /// </summary>
	public sealed class HeaderInput : IMessageInput
	{
        private readonly IEnumerable<IPair<string, string>> headers;

        /// <summary>
        /// Header input for a <see cref="IMessage"/>.
        /// </summary>
        public HeaderInput(params IPair<string, string>[] headers) : this(
            AsEnumerable._(headers)
        )
        { }

        /// <summary>
        /// Header input for a <see cref="IMessage"/>.
        /// </summary>
        public HeaderInput(IEnumerable<IPair<string,string>> headers)
		{
            this.headers = headers;
        }

        public IMessage WriteTo(IMessage message)
        {
            foreach (var header in this.headers)
                message = message.With(header);
            return message;
        }
    }
}

