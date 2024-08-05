using Tonga.Map;

namespace WHyLL.Headers
{
	/// <summary>
	/// Message Header.
	/// </summary>
	public sealed class Header(string name, Func<string> value) : 
		PairEnvelope<string,string>(AsPair._(name, value))
	{
        /// <summary>
        /// Message Header.
        /// </summary>
        public Header(string name, string value) : this(name, () => value)
        { }
	}
}

