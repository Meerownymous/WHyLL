using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
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
            uri, new Version(1,1), body, headers
        )
        { }

        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(Uri uri, Version httpVersion, Stream body, params IPair<string, string>[] headers) : this(
            uri, httpVersion, body, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(Uri uri, Stream body, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), body, input, more
        )
        { }

        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(Uri uri, Version httpVersion, Stream body, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("PUT", uri, httpVersion).AsString(),
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