using Tonga;
using Tonga.Enumerable;
using Whyre.Message;
using Whyre.MessageInput;

namespace Whyre.Request
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
            uri, new Version(1,1), body, headers
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Version httpVersion, Stream body, params IPair<string, string>[] headers) : this(
            uri, httpVersion, body, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), body, input, more
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Version httpVersion, Stream body, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("POST", uri, httpVersion).AsString(),
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