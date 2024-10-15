using Tonga;
using Tonga.Enumerable;
using WHyLL.Http.Request;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http3.Request
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get(string url, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("GET", url, new Version(2, 0)).AsString(),
                        None._<IPair<string,string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url, params IPair<string, string>[] headers) : this(
            url, new HeaderInput(headers)
        )
        { }
    }
}

