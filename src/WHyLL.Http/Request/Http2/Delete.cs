﻿using Tonga;
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
            new MessageWithInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestPrologue("DELETE", url, new Version(2, 0))
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

