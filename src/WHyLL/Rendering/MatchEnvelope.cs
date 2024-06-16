using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Envelope for matches to use in <see cref="Switch{TOutput}"/>.
    /// </summary>
    public abstract class MatchEnvelope<TOutput> : IMatch<TOutput>
    {
        private readonly IMatch<TOutput> match;

        /// <summary>
        /// Envelope for matches to use in <see cref="Switch{TOutput}"/>.
        /// </summary>
        public MatchEnvelope(IMatch<TOutput> match)
        {
            this.match = match;
        }

        public IRendering<TOutput> Consequence(
            string firstLine,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        ) =>
            this.match.Consequence(firstLine, parts, body);

        public bool Matches(
            string firstLine,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        ) =>
            this.match.Matches(firstLine, parts, body);
    }
}

