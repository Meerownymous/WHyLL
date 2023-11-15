using Tonga;

namespace Whyre.Parts
{
	/// <summary>
	/// Path header.
	/// </summary>
	public sealed class Path : PartWrap
	{
        /// <summary>
        /// Path header.
        /// </summary>
        public Path() : this(string.Empty)
        { }

        /// <summary>
        /// Path header.
        /// </summary>
        public Path(string value) : base("path", value)
		{ }

        /// <summary>
        /// Path header.
        /// </summary>
        public Path(IMap<string,string> request) : base("path", request)
        { }
    }
}

