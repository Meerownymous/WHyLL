using Tonga;

namespace WHyLL.Warp
{
    /// <summary>
    /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
    /// </summary>
    public sealed class Case<TOutput>(
        Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match, 
        IWarp<TOutput> consequence
    ) : IMatch<TOutput>
    {
        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<string, bool> match, IWarp<TOutput> consequence) : this(
            (firstLine, _, _) => match(firstLine), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<IEnumerable<IPair<string, string>>, bool> match, IWarp<TOutput> consequence) : this(
            (_, parts, _) => match(parts), consequence
        )
        { }

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public Case(Func<Stream, bool> match, IWarp<TOutput> consequence) : this(
            (_, _, body) => match(body), consequence
        )
        { }

        public IWarp<TOutput> Consequence(
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
            Func<string, bool> match, IWarp<TOutput> consequence
        ) =>
            new(match, consequence);

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<IEnumerable<IPair<string, string>>, bool> match, IWarp<TOutput> consequence
        ) =>
            new(match, consequence);

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<Stream, bool> match, IWarp<TOutput> consequence
        ) =>
            new(match, consequence);

        /// <summary>
        /// Case to be inserted into a <see cref="Switch{TOutput}"/>.
        /// </summary>
        public static Case<TOutput> _<TOutput>(
            Func<string, IEnumerable<IPair<string, string>>, Stream, bool> match, IWarp<TOutput> consequence
        ) =>
            new(match, consequence);
    }
}

