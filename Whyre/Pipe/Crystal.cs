using System;
using Tonga;
using Tonga.Map;

namespace Whyre.Wire
{
    /// <summary>
    /// Crystallization, where input crystallizes in specific actions.
    /// </summary>
    public sealed class Crystal : IAction<IPair<string,string>>
	{
        private readonly DeepMap<IPair<string,string>, string, Action<IPair<string, string>>> line;

        /// <summary>
        /// Crystallization, where input crystallizes in specific actions.
        /// </summary>
        public Crystal(
			IEnumerable<IPair<IPair<string,string>, Action<IPair<string,string>>>> spikes
		)
		{
            this.line =
                DeepMap._(
                    part => part.Key(),
                    AsMap._(spikes)
                );
        }

        public void Invoke(IPair<string,string> part)
        {
            this.line[part].Invoke(part);
        }
    
        public static Crystal _(
            IEnumerable<IPair<IPair<string, string>, Action<IPair<string, string>>>> spikes
        ) =>
            new Crystal(spikes);

        public static Crystal _(
            params IPair<IPair<string, string>, Action<IPair<string, string>>>[] spikes
        ) =>
            new Crystal(spikes);
    }
}

