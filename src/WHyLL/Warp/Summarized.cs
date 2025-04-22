using Tonga.Enumerable;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Summarizes the outputs of multiple warps.
    /// </summary>
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
            results => results[^1],
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

namespace WHyLL
{
    public static class SummarizedSmarts
    {
        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(params IWarp<TOutput>[] chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(IEnumerable<IWarp<TOutput>> chain) =>
            new(chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(Func<TOutput[], TOutput> summarize, params IWarp<TOutput>[] chain) =>
            new(summarize, chain);

        /// <summary>
        /// Summarized chain of renderings. Result of the last rendering is returned.
        /// </summary>
        public static Summarized<TOutput> _<TOutput>(
            Func<TOutput[], TOutput> summarize,
            IEnumerable<IWarp<TOutput>> chain
        ) =>
            new(summarize, chain);
        
        public static Task<TOutput> Summarized<TOutput>(
            this IMessage message,
            Func<TOutput[], TOutput> summarize,
            IEnumerable<IWarp<TOutput>> chain
        ) => message.To(new Summarized<TOutput>(summarize, chain));
        
        public static Task<TOutput> Summarized<TOutput>(
            this IMessage message,
            params IWarp<TOutput>[] chain
        ) => message.To(new Summarized<TOutput>(chain));
        
        public static Task<TOutput> Summarized<TOutput>(
            this IMessage message,
            IEnumerable<IWarp<TOutput>> chain
        ) => message.To(new Summarized<TOutput>(chain));
        
        public static Task<TOutput> Summarized<TOutput>(
            this IMessage message,
            Func<TOutput[], TOutput> summarize,
            params IWarp<TOutput>[] chain
        ) => message.To(new Summarized<TOutput>(chain));
    }
}