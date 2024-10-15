using Tonga;
using Tonga.Enumerable;
using Tonga.Text;
using WHyLL.Message;
using WHyLL.MessageInput;
using Joined = Tonga.Enumerable.Joined;

namespace WHyLL.Http.Request
{
    /// <summary>
    /// HTTP GET Request.
    /// </summary>
    public sealed class Get(Func<string> url, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    inputs,
                    new SimpleMessageInput(
                        new RequestLine(new AsText("GET"), new AsText(url()), httpVersion).AsString(),
                        None._<IPair<string,string>>(),
                        new MemoryStream()
                    )
                )
            )
        )
    {
        public Get(string url, Version httpVersion, IEnumerable<IMessageInput> inputs) : 
            this(() => url, httpVersion, inputs)
        { }
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url, Version httpVersion, IMessageInput input, params IMessageInput[] inputs) :
            this(url, httpVersion, Joined._(inputs, input))
        { }
        
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url, IPair<string,string> header, params IPair<string, string>[] headers) : this(
            url, new Version(1,1), new HeaderInput(Joined._(headers, header))
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url, Version httpVersion, IPair<string,string> header, params IPair<string, string>[] headers) : this(
            url, httpVersion, new HeaderInput(Joined._(headers, header))
        )
        { }
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url, Version httpVersion) : this(
            url, httpVersion, []
        )
        { }
        
        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url) : this(
            url, new Version(1,1), []
        )
        { }

        /// <summary>
        /// HTTP GET Request.
        /// </summary>
        public Get(string url, IMessageInput input, params IMessageInput[] more) : this(
            url, new Version(1,1), input, more
        )
        { }
    }
}

