﻿using System.Net;
using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Http.Response
{
    /// <summary>
    /// Success response.
    /// </summary>
    public sealed class Success : MessageEnvelope
    {
        /// <summary>
        /// Success response.
        /// </summary>
        public Success() : base(
            new SimpleMessage(
                new ResponseLine((int)HttpStatusCode.OK),
                new None<IPair<string, string>>(),
                new MemoryStream(0)
            )
        )
        { }
    }
}

