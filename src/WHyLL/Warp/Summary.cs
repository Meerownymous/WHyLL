using Tonga.Enumerable;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Summarizes the outputs of multiple warps.
    /// </summary>
    public sealed class Summary<TOutput>(Func<TOutput[], TOutput> summarize, IEnumerable<IWarp<TOutput>> chain) : 
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
        public Summary(params IWarp<TOutput>[] chain) : this(
            results => results[^1], chain.AsEnumerable()
        )
        { }

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public Summary(IEnumerable<IWarp<TOutput>> chain) : this(
            results => results[^1],
            chain
        )
        { }

        /// <summary>
        /// Summarized chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public Summary(Func<TOutput[], TOutput> summarize, params IWarp<TOutput>[] chain) : this(
            summarize, chain.AsEnumerable()
        )
        { }
    }
}

namespace WHyLL
{
    public static class WarpSmarts
    {
        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summary<TOutput> AsSummary<TOutput>(params IWarp<TOutput>[] chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summary<TOutput> AsSummary<TOutput>(IEnumerable<IWarp<TOutput>> chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summary<TOutput> AsSummary<TOutput>(Func<TOutput[], TOutput> summarize, params IWarp<TOutput>[] chain) =>
            new(summarize, chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summary<TOutput> AsSummary<TOutput>(
            Func<TOutput[], TOutput> summarize,
            IEnumerable<IWarp<TOutput>> chain
        ) =>
            new(summarize, chain);
        
        public static Task<TOutput> AsSummary<TOutput>(
            this IMessage message,
            Func<TOutput[], TOutput> summarize,
            IEnumerable<IWarp<TOutput>> chain
        ) => message.To(new Summary<TOutput>(summarize, chain));
        
        public static Task<TOutput> AsSummary<TOutput>(
            this IMessage message,
            params IWarp<TOutput>[] chain
        ) => message.To(new Summary<TOutput>(chain));
        
        public static Task<TOutput> AsSummary<TOutput>(
            this IMessage message,
            IEnumerable<IWarp<TOutput>> chain
        ) => message.To(new Summary<TOutput>(chain));
        
        public static Task<TOutput> AsSummary<TOutput>(
            this IMessage message,
            Func<TOutput[], TOutput> summarize,
            params IWarp<TOutput>[] chain
        ) => message.To(new Summary<TOutput>(summarize, chain));
    }
}