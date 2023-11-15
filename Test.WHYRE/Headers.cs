using System;
using Tonga;
using Tonga.Collection;
using Tonga.Map;
using Tonga.Text;
using Whyre;
using Whyre.Parts;

namespace WHYRE.Test
{
    public sealed class ResponseHeaders : IRendering<IMap<string, ICollection<string>>>
	{
        private readonly IMap<string, ICollection<string>> before;

        public ResponseHeaders() : this(Tonga.Map.Empty._<string, ICollection<string>>())
        { }

        private ResponseHeaders(IMap<string,ICollection<string>> before)
		{
            this.before = before;
        }

        public IMap<string, ICollection<string>> Render(Stream body)
        {
            return this.before;
        }

        public IRendering<IMap<string, ICollection<string>>> With(string name, string value)
        {
            var result = before;
            if(Is._(new Header(), name).Value())
            {
                name = new TrimmedLeft(name, "header:").AsString();
                if (!before.Keys().Contains(name))
                    result = before.With(AsPair._(name, AsCollection._(value)));
                else
                    result = before.With(
                        AsPair._(name, Tonga.Collection.Joined._(before[name], Tonga.Enumerable.Single._(value)))
                    );
            }
            return new ResponseHeaders(result);
        }
    }
}

