using System;
namespace Whyre.Parts
{
	/// <summary>
	/// Query param in uri.
	/// </summary>
	public sealed class Query : PartWrap
	{
        /// <summary>
        /// Query param in uri.
        /// </summary>
        public Query() : base($"query", String.Empty)
        { }

        /// <summary>
        /// Query param in uri.
        /// </summary>
        public Query(string value) : base(
			$"query", value
		)
		{ }
	}
}

