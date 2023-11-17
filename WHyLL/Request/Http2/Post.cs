using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;
using WHyLL.Request;

namespace WHyLL.Request.Http2
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public sealed class Post : MessageEnvelope
    {
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body, params IPair<string, string>[] headers) : this(
            uri, body, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("POST", uri, new Version(2, 0)).AsString(),
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