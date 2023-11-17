using Tonga.Map;

namespace WHyLL.Headers
{
	/// <summary>
	/// HTTP Message Header.
	/// </summary>
	public sealed class Header : PairEnvelope<string,string>
	{
        /// <summary>
        /// HTTP Message Header.
        /// </summary>
        public Header(string name, string value) : this(name, () => value)
        { }

        /// <summary>
        /// HTTP Message Header.
        /// </summary>
        public Header(string name, Func<string> value) : base(AsPair._(name, value))
		{ }
	}
}

