using Tonga.Enumerable;
using WHyLL;
using WHyLL.Warp;

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
            chain.AsEnumerable()
        )
        { }
    }
}

public static class ChainSmarts
{
    public static Task<TOutput[]> Chain<TOutput>(
        this IMessage message, IEnumerable<IWarp<TOutput>> chain
    ) =>
        message.To(new Chain<TOutput>(chain));
    
    public static Task<TOutput[]> Chain<TOutput>(
        this IMessage message, params IWarp<TOutput>[] chain
    ) =>
        message.To(new Chain<TOutput>(chain));
}