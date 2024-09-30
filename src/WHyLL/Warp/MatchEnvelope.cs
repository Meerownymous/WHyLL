using Tonga;

namespace WHyLL.Warp
{
    /// <summary>
    /// Envelope for matches to use in <see cref="Switch{TOutput}"/>.
    /// </summary>
    public abstract class MatchEnvelope<TOutput>(IMatch<TOutput> match) : IMatch<TOutput>
    {
        public IWarp<TOutput> Consequence(
            string firstLine,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        ) =>
            match.Consequence(firstLine, parts, body);

        public bool Matches(
            string firstLine,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        ) =>
            match.Matches(firstLine, parts, body);
    }
}

