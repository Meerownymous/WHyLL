using System;
using Tonga;

namespace WHyLL.Message
{
    /// <summary>
    /// Simple <see cref="IMessageInput"/>
    /// </summary>
	public sealed class SimpleMessageInput : IMessageInput
	{
        private readonly string firstLine;
        private readonly IEnumerable<IPair<string, string>> headers;
        private readonly Stream body;

        /// <summary>
        /// Simple <see cref="IMessageInput"/>
        /// </summary>
        public SimpleMessageInput(string firstLine, IEnumerable<IPair<string,string>> headers, Stream body)
		{
            this.firstLine = firstLine;
            this.headers = headers;
            this.body = body;
        }

        public IMessage WriteTo(IMessage message)
        {
            message = message.With(this.firstLine);
            foreach (var header in this.headers)
                message = message.With(header);
            message = message.WithBody(this.body);
            return message;
        }
    }
}

