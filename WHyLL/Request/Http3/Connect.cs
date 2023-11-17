using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;
using WHyLL.Request;

namespace WHyLL.Http3
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
            uri, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP CONNECT Request.
        /// </summary>
        public Connect(Uri uri, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("CONNECT", uri, new Version(3,0)).AsString(),
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

