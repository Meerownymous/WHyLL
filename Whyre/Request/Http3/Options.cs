using Tonga;
using Tonga.Enumerable;
using Whyre.Message;
using Whyre.MessageInput;
using Whyre.Request;

namespace Whyre.Request.Http3
{
    /// <summary>
    /// HTTP OPTIONS Request.
    /// </summary>
    public sealed class Options : MessageEnvelope
    {
        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(Uri uri, params IPair<string, string>[] headers) : this(
            uri, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(Uri uri, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("OPTIONS", uri, new Version(3, 0)).AsString(),
                        None._<IPair<string, string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
        { }
    }
}

