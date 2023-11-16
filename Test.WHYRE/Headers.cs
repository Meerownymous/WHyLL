using System;
using Tonga;
using Tonga.Collection;
using Tonga.Map;
using Tonga.Text;
using Whyre;
using Whyre.Parts;

namespace Whyre.Test
{
    /// <summary>
    /// Renders the Headers of a message.
    /// </summary>
    public sealed class Headers : IRendering<IMap<string, ICollection<string>>>
	{
        private readonly IMap<string, ICollection<string>> before;

        /// <summary>
        /// Renders the Headers of a message.
        /// </summary>
        public Headers() : this(Tonga.Map.Empty._<string, ICollection<string>>())
        { }

        /// <summary>
        /// Renders the Headers of a message.
        /// </summary>
        private Headers(IMap<string,ICollection<string>> before)
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

        public IRendering<IMap<string, ICollection<string>>> Refine(IPair<string,string> part)
        {
            var result = before;
            var name = part.Key();
                if (!before.Keys().Contains(name))
                    result = before.With(AsPair._(name, AsCollection._(part.Value())));
                else
                    result = before.With(
                        AsPair._(name, Tonga.Collection.Joined._(before[name], Tonga.Enumerable.Single._(part.Value())))
                    );
            
            return new Headers(result);
        }
    }
}

