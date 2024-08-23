using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get(Uri uri, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    inputs,
                    new SimpleMessageInput(
                        new RequestLine("GET", uri, httpVersion).AsString(),
                        None._<IPair<string,string>>(),
                        new MemoryStream()
                    )
                )
            )
        )
    {
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, Version httpVersion, IMessageInput input, params IMessageInput[] inputs) :
            this(uri, httpVersion, Joined._(inputs, input))
        { }
        
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, IPair<string,string> header, params IPair<string, string>[] headers) : this(
            uri, new Version(1,1), new HeaderInput(Joined._(headers, header))
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, Version httpVersion, IPair<string,string> header, params IPair<string, string>[] headers) : this(
            uri, httpVersion, new HeaderInput(Joined._(headers, header))
        )
        { }
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, Version httpVersion) : this(
            uri, httpVersion, []
        )
        { }
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri) : this(
            uri, new Version(1,1), []
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), input, more
        )
        { }
    }
}

