﻿using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Request.Http3
{
    /// <summary>
    /// HTTP OPTIONS Request.
    /// </summary>
    public sealed class Options(string url, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("OPTIONS", url, new Version(3, 0)).AsString(),
                        None._<IPair<string, string>>(),
                        new MemoryStream()
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP OPTIONS Request.
        /// </summary>
        public Options(string url, params IPair<string, string>[] headers) : this(
            url, new HeaderInput(headers)
        )
        { }
    }
}

