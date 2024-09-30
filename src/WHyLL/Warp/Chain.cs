using Tonga.Enumerable;

namespace WHyLL.Warp
{
    /// <summary>
    /// Chain of Warps. Result of the last Warp is returned.
    /// </summary>
    public sealed class Chain<TOutput>(IEnumerable<IWarp<TOutput>> chain) : 
        WarpEnvelope<TOutput[]>(
            new MessageAs<TOutput[]>(async (msg) =>
            {
                var results = new List<TOutput>();
                foreach (var Warp in chain)
                    results.Add(await msg.To(Warp));
                return results.ToArray();
            })
        )
    {
        /// <summary>
        /// Chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public Chain(params IWarp<TOutput>[] chain) : this(
            AsEnumerable._(chain)
        )
        { }
    }

    /// <summary>
    /// Chain of Warps. Result of the last Warp is returned.
    /// </summary>
    public static class Chain
    {
        /// <summary>
        /// Chain of Warps. Result of the last Warp is returned.
        /// </summary>
        public static Chain<TOutput> _<TOutput>(IEnumerable<IWarp<TOutput>> chain) =>
            new(chain);
    }
}

