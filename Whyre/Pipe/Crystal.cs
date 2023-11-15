using System;
using Tonga;
using Tonga.Map;

namespace Whyre.Wire
{
    /// <summary>
    /// Crystallization, where input crystallizes in specific actions.
    /// </summary>
    public sealed class Crystal<Input, Attribute> : IAction<Input>
	{
        private readonly DeepMap<Input, Attribute, Action<Input>> line;

        /// <summary>
        /// Crystallization, where input crystallizes in specific actions.
        /// </summary>
        public Crystal(
            Func<Input, Attribute> extract,
			IEnumerable<IPair<Input, Action<Input>>> spikes
		)
		{
            this.line =
                DeepMap._(
                    extract,
                    AsMap._(spikes)
                );
        }

        public void Invoke(Input input)
        {
            this.line[input].Invoke(input);
        }
    }

    public static class Crystal
    {
        public static Crystal<Input, Attribute> _<Input, Attribute>(
            Func<Input, Attribute> extract,
            IEnumerable<IPair<Input, Action<Input>>> lines
        ) =>
            new Crystal<Input, Attribute>(extract, lines);

        public static Crystal<Input, Attribute> _<Input, Attribute>(
            Func<Input, Attribute> extract,
            params IPair<Input, Action<Input>>[] lines
        ) =>
            new Crystal<Input, Attribute>(extract, lines);
    }
}

