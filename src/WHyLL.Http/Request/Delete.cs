using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP DELETE Request.
    /// </summary>
    public sealed class Delete(string uri, Version httpVersion, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageWithInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("DELETE", uri, httpVersion).AsString(),
                        None._<IPair<string, string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(string url, params IPair<string, string>[] headers) : this(
            url, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(string url, Version httpVersion, params IPair<string, string>[] headers) : this(
            url, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(string url, IMessageInput input, params IMessageInput[] more) : this(
            url, new Version(1,1), input, more
        )
        { }
    }
}

