using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public sealed class Post(Uri uri, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("POST", uri, httpVersion).AsString()
                    ),
                    inputs
                )
            )
        )
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
            uri, 
            httpVersion, 
            Joined._<IMessageInput>(AsEnumerable._(new BodyInput(body)), 
            new HeaderInput(headers))
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body, params IMessageInput[] more) : this(
            uri, new Version(1,1), Joined._(AsEnumerable._(new BodyInput(body)), more)
        )
        { }
        
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, params IMessageInput[] more) : this(
            uri, new Version(1,1), more
        )
        { }
    }
}