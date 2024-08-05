using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Envelope for renderings.
    /// </summary>
    public abstract class RenderingEnvelope<TOutput>(IRendering<TOutput> origin) : IRendering<TOutput>
    {
        public IRendering<TOutput> Refine(string start) =>
            origin.Refine(start);

        public IRendering<TOutput> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IRendering<TOutput> Refine(params IPair<string, string>[] parts) =>
            origin.Refine(parts);

        public IRendering<TOutput> Refine(Stream body) =>
            origin.Refine(body);

        public Task<TOutput> Render() =>
            origin.Render();
    }
}

