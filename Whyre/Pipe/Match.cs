using System;
using Tonga;
using Tonga.Map;

namespace Whyre.Wire
{
	/// <summary>
	/// Construction match for using with <see cref="Crystal{Input, Attribute}"/>
	/// </summary>
	public sealed class When : PairEnvelope<IPair<string,string>, Action<IPair<string,string>>>
	{
        /// <summary>
        /// Construction match for using with <see cref="Crystal{Input, Attribute}"/>
        /// </summary>
        public When(IPair<string,string> key, Action<IPair<string,string>> act) : base(AsPair._(key, act))
		{ }

        /// <summary>
        /// Construction match for using with <see cref="Crystal{Input, Attribute}"/>
        /// </summary>
        public static When _(IPair<string, string> key, Action<IPair<string,string>> act) =>
            new When(key, act);
    }
}