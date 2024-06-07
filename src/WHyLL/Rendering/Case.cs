using Tonga;
using WHyLL;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
    /// </summary>
    public sealed class Case<TOutput> : IMatch<TOutput>
    {
        private readonly Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match;
        private readonly IRendering<TOutput> consequence;

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<string, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, parts, body) => match(firstLine), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<IEnumerable<IPair<string, string>>, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, parts, body) => match(parts), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<Stream, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, parts, body) => match(body), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match, IRendering<TOutput> consequence)
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

    /// <summary>
    /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
    /// </summary>
    public static class Case
    {
        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<string, bool> match, IRendering<TOutput> consequence
        ) =>
            new Case<TOutput>(match, consequence);

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<IEnumerable<IPair<string, string>>, bool> match, IRendering<TOutput> consequence
        ) =>
            new Case<TOutput>(match, consequence);

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<Stream, bool> match, IRendering<TOutput> consequence
        ) =>
            new Case<TOutput>(match, consequence);

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match, IRendering<TOutput> consequence
        ) =>
            new Case<TOutput>(match, consequence);
    }
}

