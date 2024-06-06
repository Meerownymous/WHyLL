using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Envelope for renderings.
    /// </summary>
    public abstract class RenderingEnvelope<TOutput> : IRendering<TOutput>
    {
        private readonly IRendering<TOutput> origin;

        /// <summary>
        /// Envelope for renderings.
        /// </summary>
        public RenderingEnvelope(IRendering<TOutput> origin)
        {
            this.origin = origin;
        }

        public IRendering<TOutput> Refine(string start) =>
            this.origin.Refine(start);

        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) =>
            this.origin.Refine(parts);

        public IRendering<TOutput> Refine(Stream body) =>
            this.origin.Refine(body);

        public Task<TOutput> Render() =>
            this.origin.Render();
    }
}

