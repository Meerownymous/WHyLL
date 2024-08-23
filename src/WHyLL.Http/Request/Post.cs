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
        public Post(Uri uri, Stream body, IPair<string,string> header, params IPair<string, string>[] headers) : this(
            uri, new Version(1,1), body, Joined._(headers, header)
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Version httpVersion, Stream body, IEnumerable<IPair<string, string>> headers) : this(
            uri, 
            httpVersion, 
            Joined._<IMessageInput>(AsEnumerable._(new BodyInput(body)), 
            new HeaderInput(headers))
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), Joined._(AsEnumerable._(new BodyInput(body)), Joined._(more, input))
        )
        { }
        
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body) : this(
            uri, new Version(1,1), Joined._(AsEnumerable._(new BodyInput(body)))
        )
        { }
        
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), Joined._(more, input)
        )
        { }
    }
}