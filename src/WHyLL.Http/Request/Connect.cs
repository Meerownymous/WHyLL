using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP CONNECT Request.
    /// </summary>
    public sealed class Connect : MessageEnvelope
    {
        /// <summary>
        /// HTTP CONNECT Request.
        /// </summary>
        public Connect(Uri uri, params IPair<string, string>[] headers) : this(
            uri, new Version(1,1), new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP CONNECT Request.
        /// </summary>
        public Connect(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP CONNECT Request.
        /// </summary>
        public Connect(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), input, more
        )
        { }

        /// <summary>
        /// HTTP CONNECT Request.
        /// </summary>
        public Connect(Uri uri, Version httpVersion, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("CONNECT", uri, httpVersion).AsString(),
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

