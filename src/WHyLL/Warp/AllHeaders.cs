using Tonga;
using Tonga.Collection;
using Tonga.Map;
using WHyLL.Warp;

namespace WHyLL.Warp
{
    /// <summary>
    /// Renders the Headers of a message.
    /// </summary>
    public sealed class AllHeaders(IMap<string,ICollection<string>> before) : 
        IWarp<IMap<string, ICollection<string>>>
	{

        /// <summary>
        /// Renders the Headers of a message.
        /// </summary>
        public AllHeaders() : this(Tonga.Map.Empty._<string, ICollection<string>>())
        { }

        public IWarp<IMap<string, ICollection<string>>> Refine(string newFirstLine) =>
            this;

        public Task<IMap<string, ICollection<string>>> Render() =>
            Task.FromResult(before);

        public IWarp<IMap<string, ICollection<string>>> Refine(Stream body) =>
            this;

        public IWarp<IMap<string, ICollection<string>>> Refine(IEnumerable<IPair<string, string>> newParts) =>
            Refine(newParts.ToArray());

        public IWarp<IMap<string, ICollection<string>>> Refine(params IPair<string,string>[] parts)
        {
            var result = before;
            foreach (var part in parts)
            {
                var name = part.Key();
                result = result.With(!result.Keys().Contains(name) 
                    ? AsPair._(name, AsCollection._(part.Value())) 
                    : AsPair._(name, Joined._(result[name], Tonga.Enumerable.Single._(part.Value()))));
            }
            
            return new AllHeaders(result);
        }
    }
}

namespace WHyLL
{
    public static class AllHeadersSmarts
    {
        public static async Task<IMap<string, ICollection<string>>> AllHeaders(this IMessage msg) =>
            await msg.To(new AllHeaders());
    }
}