using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP OPTIONS Request.
    /// </summary>
    public sealed class Options : MessageEnvelope
    {
        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(Uri uri, params IPair<string, string>[] headers) : this(
            uri, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), input, more
        )
        { }

        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(Uri uri, Version httpVersion, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("OPTIONS", uri, httpVersion).AsString(),
                        None._<IPair<string, string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
        { }
    }
}

