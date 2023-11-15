using System;
using Tonga;
using Tonga.Text;

namespace Whyre.Parts
{
	/// <summary>
	/// Specific part content.
	/// </summary>
	public sealed class Content : TextEnvelope
	{
        /// <summary>
        /// Specific part content.
        /// </summary>
        public Content(IPair<string,string> part, IMap<string,string> request) : base(
			AsText._(request[part.Key()])
		)
		{ }
	}
}

