using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Request.Http3
{
    /// <summary>
    /// HTTP HEAD Request.
    /// </summary>
    public sealed class Head : MessageEnvelope
    {
        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(Uri uri, params IPair<string, string>[] headers) : this(
            uri, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(Uri uri, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("HEAD", uri, new Version(3, 0)).AsString(),
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

