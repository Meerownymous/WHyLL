using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP PUT Request.
    /// </summary>
    public sealed class Put(string url, Version httpVersion, Stream body, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageWithInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("PUT", url, httpVersion).AsString(),
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
            url, new Version(1,1), body, headers
        )
        { }

        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(string url, Version httpVersion, Stream body, params IPair<string, string>[] headers) : this(
            url, httpVersion, body, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP PUT Request.
        /// </summary>
        public Put(string url, Stream body, IMessageInput input, params IMessageInput[] more) : this(
            url, new Version(1,1), body, input, more
        )
        { }
    }
}