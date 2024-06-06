using Tonga;
using Tonga.Collection;
using Tonga.Map;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the Headers of a message.
    /// </summary>
    public sealed class AllHeaders : IRendering<IMap<string, ICollection<string>>>
	{
        private readonly IMap<string, ICollection<string>> before;

        /// <summary>
        /// Renders the Headers of a message.
        /// </summary>
        public AllHeaders() : this(Tonga.Map.Empty._<string, ICollection<string>>())
        { }

        /// <summary>
        /// Renders the Headers of a message.
        /// </summary>
        private AllHeaders(IMap<string,ICollection<string>> before)
		{
            this.before = before;
        }

        public IRendering<IMap<string, ICollection<string>>> Refine(string firstLine)
        {
            return this;
        }

        public Task<IMap<string, ICollection<string>>> Render()
        {
            return Task.FromResult(this.before);
        }

        public IRendering<IMap<string, ICollection<string>>> Refine(Stream body)
        {
            return this;
        }

        public IRendering<IMap<string, ICollection<string>>> Refine(IEnumerable<IPair<string, string>> parts) =>
            Refine(parts.ToArray());

        public IRendering<IMap<string, ICollection<string>>> Refine(params IPair<string,string>[] parts)
        {
            var result = this.before;
            foreach (var part in parts)
            {
                var name = part.Key();
                if (!result.Keys().Contains(name))
                    result = result.With(AsPair._(name, AsCollection._(part.Value())));
                else
                    result = result.With(
                        AsPair._(name, Joined._(result[name], Tonga.Enumerable.Single._(part.Value())))
                    );
            }
            
            return new AllHeaders(result);
        }
    }
}

