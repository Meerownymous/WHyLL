using Tonga;
using Tonga.Enumerable;
using WHyLL.Http.Request;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http2.Request
{
    /// <summary>
    /// HTTP PUT Request.
    /// </summary>
    public sealed class Put(string url, Stream body, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("PUT", url, new Version(2, 0)).AsString(),
                        None._<IPair<string, string>>(),
                        body
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(string url, Stream body, params IPair<string, string>[] headers) : this(
            url, body, new HeaderInput(headers)
        )
        { }
    }
}