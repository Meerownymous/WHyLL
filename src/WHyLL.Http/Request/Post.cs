using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public sealed class Post(string url, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("POST", url, httpVersion).AsString()
                    ),
                    inputs
                )
            )
        )
    {
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(string url, Stream body, IPair<string,string> header, params IPair<string, string>[] headers) : this(
            url, new Version(1,1), body, Joined._(headers, header)
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(string url, Version httpVersion, Stream body, IEnumerable<IPair<string, string>> headers) : this(
            url, 
            httpVersion, 
            Joined._<IMessageInput>(AsEnumerable._(new BodyInput(body)), 
            new HeaderInput(headers))
        )
        { }

        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(string url, Stream body, IMessageInput input, params IMessageInput[] more) : this(
            url, new Version(1,1), Joined._(AsEnumerable._(new BodyInput(body)), Joined._(more, input))
        )
        { }
        
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(string url, Stream body) : this(
            url, new Version(1,1), Joined._(AsEnumerable._(new BodyInput(body)))
        )
        { }
        
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(string url, IMessageInput input, params IMessageInput[] more) : this(
            url, new Version(1,1), Joined._(more, input)
        )
        { }
    }
}