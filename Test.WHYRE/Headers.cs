using System;
using Tonga;
using Tonga.Collection;
using Tonga.Map;
using Tonga.Text;
using Whyre;
using Whyre.Parts;

namespace WHYRE.Test
{
    public sealed class Headers : IRendering<IMap<string, ICollection<string>>>
	{
        private readonly IMap<string, ICollection<string>> before;

        public Headers() : this(Tonga.Map.Empty._<string, ICollection<string>>())
        { }

        private Headers(IMap<string,ICollection<string>> before)
		{
            this.before = before;
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
            if(Is._(new Header(), part.Key()).Value())
            {
                var name = new TrimmedLeft(part.Key(), "header:").AsString();
                if (!before.Keys().Contains(name))
                    result = before.With(AsPair._(name, AsCollection._(part.Value())));
                else
                    result = before.With(
                        AsPair._(name, Tonga.Collection.Joined._(before[name], Tonga.Enumerable.Single._(part.Value())))
                    );
            }
            return new Headers(result);
        }
    }
}

