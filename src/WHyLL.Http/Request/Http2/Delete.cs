using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http2
{
    /// <summary>
    /// HTTP DELETE Request.
    /// </summary>
    public sealed class Delete(string url, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("DELETE", url, new Version(2, 0)).AsString(),
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
            url, new HeaderInput(headers)
        )
        { }
    }
}

