using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Headers;
using WHyLL.Message;

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

namespace WHyLL
{
	public static class HeaderSmarts
	{
		public static IMessage Header(this IMessage message, string name, string value) =>
			message.With(new Header(name, value));
		public static IMessage Header(this IMessage message, string name, params string[] values) =>
			message.With(
				values.Mapped(value => new Header(name, value))
			);
	}
		
}