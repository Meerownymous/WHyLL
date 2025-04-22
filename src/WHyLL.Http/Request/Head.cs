using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP HEAD Request.
    /// </summary>
    public sealed class Head(string url, Version httpVersion, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageWithInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("HEAD", url, httpVersion).AsString(),
                        None._<IPair<string, string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(string url, params IPair<string, string>[] headers) : this(
            url, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(string url, Version httpVersion, params IPair<string, string>[] headers) : this(
            url, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP HEAD Request.
        /// </summary>
        public Head(string url, IMessageInput input, params IMessageInput[] more) : this(
            url, new Version(1,1), input, more
        )
        { }
    }
}

