using Tonga.Enumerable;

namespace WHyLL.Rendering
{
    public sealed class Summarized<TOutput>(Func<TOutput[], TOutput> summarize, IEnumerable<IRendering<TOutput>> chain) : 
        RenderingEnvelope<TOutput>(
            new MessageAs<TOutput>(async (msg) =>
            {
                var results = await msg.Render(new Chain<TOutput>(chain));
                if (results.Length == 0)
                    throw new ArgumentException("Summarized needs at least one rendering.");
                return summarize(results);
            })
        )
    {
        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public Summarized(params IRendering<TOutput>[] chain) : this(
            (results) => results[results.Length-1], AsEnumerable._(chain)
        )
        { }

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public Summarized(IEnumerable<IRendering<TOutput>> chain) : this(
            (results) => results[results.Length-1],
            chain
        )
        { }

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public Summarized(Func<TOutput[], TOutput> summarize, params IRendering<TOutput>[] chain) : this(
            summarize, AsEnumerable._(chain)
        )
        { }
    }

    /// <summary>
    /// Summarized chain of renderings. Result of the last rendering is returned.
    /// </summary>
    public static class Summarized
    {
        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(params IRendering<TOutput>[] chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(IEnumerable<IRendering<TOutput>> chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(Func<TOutput[], TOutput> summarize, params IRendering<TOutput>[] chain) =>
            new(summarize, chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(
            Func<TOutput[], TOutput> summarize,
            IEnumerable<IRendering<TOutput>> chain
        ) =>
            new(summarize, chain);
    }
}

