using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Chain of renderings. Result of the last rendering is returned.
    /// </summary>
    public sealed class Chain<TOutput>(IEnumerable<IRendering<TOutput>> chain) : 
        RenderingEnvelope<TOutput[]>(
            new MessageAs<TOutput[]>(async (msg) =>
            {
                var results = new List<TOutput>();
                foreach (var rendering in chain)
                    results.Add(await msg.Render(rendering));
                return results.ToArray();
            })
        )
    {
        /// <summary>
        /// Chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public Chain(params IRendering<TOutput>[] chain) : this(
            AsEnumerable._(chain)
        )
        { }
    }

    /// <summary>
    /// Chain of renderings. Result of the last rendering is returned.
    /// </summary>
    public static class Chain
    {
        /// <summary>
        /// Chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Chain<TOutput> _<TOutput>(IEnumerable<IRendering<TOutput>> chain) =>
            new(chain);
    }
}

