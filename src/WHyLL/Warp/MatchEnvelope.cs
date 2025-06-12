using Tonga;

namespace WHyLL.Warp
{
    /// <summary>
    /// Envelope for matches to use in <see cref="Switch{TOutput}"/>.
    /// </summary>
    public abstract class MatchEnvelope<TOutput>(IMatch<TOutput> match) : IMatch<TOutput>
    {
        public IWarp<TOutput> Consequence(
            IPrologue prologue,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        ) =>
            match.Consequence(prologue, parts, body);

        public bool Matches(
            IPrologue prologue,
            IEnumerable<IPair<string, string>> parts,
            Stream body
        ) =>
            match.Matches(prologue, parts, body);
    }
}

