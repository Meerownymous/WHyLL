using System;
using Whyre.Parts;

namespace Whyre.Parts
{
	/// <summary>
	/// Header.
	/// </summary>
	public sealed class Header : PartWrap
	{
        /// <summary>
        /// Header.
        /// </summary>
        public Header() : base($"header", String.Empty)
        { }

        /// <summary>
        /// Header.
        /// </summary>
        public Header(string name, string value) : base($"header:{name}", value)
		{ }
	}
}

