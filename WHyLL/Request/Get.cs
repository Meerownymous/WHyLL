using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;
using WHyLL.Request;

namespace WHyLL.Request
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get : MessageEnvelope
    {
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, params IPair<string, string>[] headers) : this(
            uri, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), input, more
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, Version httpVersion, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("GET", uri, httpVersion).AsString(),
                        None._<IPair<string,string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
        { }
    }
}

