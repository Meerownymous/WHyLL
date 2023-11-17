﻿using Tonga.Text;

namespace Whyre.Request
{
	/// <summary>
	/// Http Method.
	/// </summary>
	public sealed class Method : TextEnvelope
	{
        /// <summary>
        /// Http Method.
        /// </summary>
        public Method(string method) : base(
            new Upper(
                new Strict(
                    method,
                    ignoreCase: true,
                    "GET", "PUT", "POST", "DELETE", "CONNECT", "OPTIONS", "TRACE", "HEAD"
                )
            )
        )
        { }
    }
}
