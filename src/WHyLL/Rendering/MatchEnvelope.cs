using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Envelope for matches to use in <see cref="Switch{TOutput}"/>.
    /// </summary>
    public abstract class MatchEnvelope<TOutput>(IMatch<TOutput> match) : IMatch<TOutput>
    {
        public IRendering<TOutput> Consequence(
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

