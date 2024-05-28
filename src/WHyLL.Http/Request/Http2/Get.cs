using Tonga;
using Tonga.Enumerable;
using WHyLL.Http.Request;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http2.Request
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
            uri, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("GET", uri, new Version(2, 0)).AsString(),
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

