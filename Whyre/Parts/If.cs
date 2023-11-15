using System;
using Tonga;
using Tonga.Enumerable;

namespace Whyre.Parts
{
	/// <summary>
	/// Includes content only when the given condition is true.
	/// </summary>
	public sealed class If : EnumerableEnvelope<IPair<string,string>>
	{
        /// <summary>
        /// Includes content only when the given condition is true.
        /// </summary>
        public If(Func<bool> condition, IPair<string, string> consequence) : this(
            condition, () => consequence
        )
        { }

        /// <summary>
        /// Includes content only when the given condition is true.
        /// </summary>
        public If(Func<bool> condition, Func<IPair<string, string>> consequence) : base(
            AsEnumerable._<IPair<string, string>>(() =>
                condition() ? Tonga.Enumerable.Single._(consequence()) : None._<IPair<string, string>>()
            )
        )
        { }

        /// <summary>
        /// Includes content only when the given condition is true.
        /// </summary>
        public If(Func<bool> condition, IEnumerable<IPair<string,string>> consequence) : base(
			AsEnumerable._(() =>
				condition() ? consequence : None._<IPair<string,string>>()
			)
		)
		{ }

        /// <summary>
        /// Includes content only when the given condition is true.
        /// </summary>
        public static If _(Func<bool> condition, Func<IPair<string, string>> consequence) =>
            new If(condition, consequence);

        /// <summary>
        /// Includes content only when the given condition is true.
        /// </summary>
        public static If _(Func<bool> condition, IEnumerable<IPair<string, string>> consequence) =>
			new If(condition, consequence);
    }
}

