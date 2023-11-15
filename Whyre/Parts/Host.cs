using System;
using Tonga;

namespace Whyre.Parts
{
	/// <summary>
	/// Host header.
	/// </summary>
	public sealed class Host : PartWrap
	{
        /// <summary>
        /// Host header.
        /// </summary>
        public Host() : this(string.Empty)
        { }

        /// <summary>
        /// Host header.
        /// </summary>
        public Host(string value) : base(
			"host", value
		)
		{ }

        /// <summary>
        /// Host header.
        /// </summary>
        public Host(IMap<string,string> request) : base(
            "host", request
        )
        { }

        public static IPair<string, string> _() => new Host();
        public static IPair<string, string> _(string value) => new Host(value);
    }
}

