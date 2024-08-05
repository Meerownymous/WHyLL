﻿using Tonga;
using Tonga.Enumerable;
using WHyLL.Http.Request;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http3.Request
{
    /// <summary>
    /// HTTP POST Request.
    /// </summary>
    public sealed class Post(Uri uri, Stream body, IMessageInput input, params IMessageInput[] more) : 
        MessageEnvelope(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("POST", uri, new Version(3, 0)).AsString(),
                        None._<IPair<string, string>>(),
                        body
                    ),
                    new Joined<IMessageInput>(input, more)
                )
            )
        )
    {
        /// <summary>
        /// HTTP POST Request.
        /// </summary>
        public Post(Uri uri, Stream body, params IPair<string, string>[] headers) : this(
            uri, body, new HeaderInput(headers)
        )
        { }
    }
}