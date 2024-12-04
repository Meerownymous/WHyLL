using Tonga.Enumerable;

namespace WHyLL.Warp
{
    public sealed class Summarized<TOutput>(Func<TOutput[], TOutput> summarize, IEnumerable<IWarp<TOutput>> chain) : 
        WarpEnvelope<TOutput>(
            new MessageAs<TOutput>(async (msg) =>
            {
                var results = await msg.To(new Chain<TOutput>(chain));
                if (results.Length == 0)
                    throw new ArgumentException("Summarized needs at least one Warp.");
                return summarize(results);
            })
        )
    {
        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public Summarized(params IWarp<TOutput>[] chain) : this(
            results => results[results.Length-1], AsEnumerable._(chain)
        )
        { }

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public Summarized(IEnumerable<IWarp<TOutput>> chain) : this(
            results => results[results.Length-1],
            chain
        )
        { }

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public Summarized(Func<TOutput[], TOutput> summarize, params IWarp<TOutput>[] chain) : this(
            summarize, AsEnumerable._(chain)
        )
        { }
    }

    /// <summary>
    /// Summarized chain of Warps. Result of the last Warp is returned.
    /// </summary>
    public static class Summarized
    {
        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(params IWarp<TOutput>[] chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(IEnumerable<IWarp<TOutput>> chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(Func<TOutput[], TOutput> summarize, params IWarp<TOutput>[] chain) =>
            new(summarize, chain);

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(
            Func<TOutput[], TOutput> summarize,
            IEnumerable<IWarp<TOutput>> chain
        ) =>
            new(summarize, chain);
    }
}

