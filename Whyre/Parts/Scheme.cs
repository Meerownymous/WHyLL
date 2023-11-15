using Tonga;

namespace Whyre.Parts
{
	/// <summary>
	/// Scheme header.
	/// </summary>
	public sealed class Scheme : PartWrap
	{
        /// <summary>
        /// Scheme header.
        /// </summary>
        public Scheme() : this(string.Empty)
        { }

        /// <summary>
        /// Scheme header.
        /// </summary>
        public Scheme(string value) : base(
            "scheme", value
		)
		{ }

        /// <summary>
        /// Host header.
        /// </summary>
        public Scheme(IMap<string, string> request) : base(
            "scheme", request
        )
        { }
    }
}

