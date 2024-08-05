using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
    /// </summary>
    public sealed class Case<TOutput>(Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match, IRendering<TOutput> consequence) : IMatch<TOutput>
    {
        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<string, bool> match, IRendering<TOutput> consequence) : this(
            (firstLine, _, _) => match(firstLine), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<IEnumerable<IPair<string, string>>, bool> match, IRendering<TOutput> consequence) : this(
            (_, parts, _) => match(parts), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<Stream, bool> match, IRendering<TOutput> consequence) : this(
            (_, _, body) => match(body), consequence
        )
        { }

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
            match(firstLine, parts, body);
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

