using Tonga;

namespace Whyre.Parts
{
	/// <summary>
	/// Port header.
	/// </summary>
	public sealed class Port : PartWrap
	{
        /// <summary>
        /// Port header.
        /// </summary>
        public Port() : this(80)
        { }

        /// <summary>
        /// Port header.
        /// </summary>
        public Port(int value) : base("port", value.ToString())
		{ }

        /// <summary>
        /// Port header.
        /// </summary>
        public Port(IMap<string,string> request) : base("port", request)
        { }
    }
}

