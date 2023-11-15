using System;
using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using Whyre.Parts;

namespace Whyre.Wire
{
    public sealed class Dispatch : IAction<IPair<string, string>>
    {
        private readonly DeepMap
            <IPair<string, string>,
            string,
            Action<string>
        > dispatch;

        public Dispatch(Func<IPair<string,string>, string> extract, params IPair<IPair<string, string>, Action<string>>[] dispatches)
        {
            this.dispatch =
                new DeepMap<IPair<string, string>, string, Action<string>>(
                    head => extract(head),
                    AsMap._(
                        AsEnumerable._(
                            dispatches
                        )
                    )
                );
        }

        public void Invoke(IPair<string, string> input)
        {
            this.dispatch[input].Invoke(input.Value());
        }


        public static IPair<IPair<string, string>, Action<string>> Case(
            IPair<string, string> match, Action<string> dispatch
        ) =>
            AsPair._(match, dispatch);
    }
}

