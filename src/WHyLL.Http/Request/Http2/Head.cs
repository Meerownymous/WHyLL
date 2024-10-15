using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http2
{
    /// <summary>
    /// HTTP HEAD Request.
    /// </summary>
    public sealed class Head(string url, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("HEAD", url, new Version(2, 0)).AsString(),
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
            url, new HeaderInput(headers)
        )
        { }
    }
}

