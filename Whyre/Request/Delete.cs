﻿using Tonga;
using Tonga.Enumerable;
using Whyre.Message;
using Whyre.MessageInput;
using Whyre.Request;

namespace Whyre.Request
{
    /// <summary>
    /// HTTP DELETE Request.
    /// </summary>
    public sealed class Delete : MessageEnvelope
    {
        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(Uri uri, params IPair<string, string>[] headers) : this(
            uri, new Version(1,1), headers
        )
        { }

        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(Uri uri, Version httpVersion, params IPair<string, string>[] headers) : this(
            uri, httpVersion, new HeaderInput(headers)
        )
        { }

        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(Uri uri, IMessageInput input, params IMessageInput[] more) : this(
            uri, new Version(1,1), input, more
        )
        { }

        /// <summary>
        /// HTTP DELETE Request.
        /// </summary>
        public Delete(Uri uri, Version httpVersion, IMessageInput input, params IMessageInput[] more) : base(
            new MessageOfInputs(
                new Joined<IMessageInput>(
                    new SimpleMessageInput(
                        new RequestLine("DELETE", uri, httpVersion).AsString(),
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
