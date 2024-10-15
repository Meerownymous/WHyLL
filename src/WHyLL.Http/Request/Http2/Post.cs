using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http2
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public sealed class Post(string url, Stream body, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("POST", url, new Version(2, 0)).AsString(),
                        None._<IPair<string, string>>(),
                        body
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(string url, Stream body, params IPair<string, string>[] headers) : this(
            url, body, new HeaderInput(headers)
        )
        { }
    }
}