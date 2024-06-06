using Tonga;
using WHyLL;

namespace WHyLL.Rendering
{
    public sealed class Conditional<TOutput> : IMatch<TOutput>
    {
        private readonly Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match;
        private readonly IRendering<TOutput> consequence;

        public Conditional(Func<string, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, parts, body) => match(firstLine), consequence
        )
        { }

        public Conditional(Func<IEnumerable<IPair<string, string>>, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, parts, body) => match(parts), consequence
        )
        { }

        public Conditional(Func<Stream, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, parts, body) => match(body), consequence
        )
        { }

        public Conditional(Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match, IRendering<TOutput> consequence)
        {
            this.match = match;
            this.consequence = consequence;
        }

        public IRendering<TOutput> Consequence(
            string firstLine,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        )
        {
            var result = consequence.Refine(firstLine).Refine(body);
            foreach (var part in parts)
                result = result.Refine(part);
            return result;
        }

        public bool Matches(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body) =>
            this.match(firstLine, parts, body);
    }
}

