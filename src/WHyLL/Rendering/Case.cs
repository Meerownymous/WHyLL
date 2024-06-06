using System;
using Tonga;
using Tonga.Map;
using WHyLL;

namespace WHyLL.Rendering
{
    public class Case<TOutput> : PairEnvelope<Func<string, IEnumerable<IPair<string, string>>, Stream, bool>,IRendering<TOutput>>
    {
        public Case(Func<string,IEnumerable<IPair<string,string>>, Stream, bool> condition, IRendering<TOutput> result) : base(
            AsPair._(condition, result)
        )
        { }
    }
}

