using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Request.Http3
{
    /// <summary>
    /// HTTP PUT Request.
    /// </summary>
    public sealed class Put : MessageEnvelope
    {
        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(Uri uri, Stream body, params IPair<string, string>[] headers) : this(
            uri, body, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(Uri uri, Stream body, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("PUT", uri, new Version(3, 0)).AsString(),
                        None._<IPair<string, string>>(),
                        body
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
        { }
    }
}