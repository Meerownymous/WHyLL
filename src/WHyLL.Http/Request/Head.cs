using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
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
            uri, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), input, more
        )
        { }


        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(Uri uri, Version httpVersion, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("HEAD", uri, httpVersion).AsString(),
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

