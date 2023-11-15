using System;
using Tonga;
using Tonga.Collection;
using Tonga.Map;
using Tonga.Text;
using Whyre;
using Whyre.Parts;

namespace WHYRE.Test
{
    public sealed class ResponseBody : IRendering<Stream>
    {
        public ResponseBody()
		{ }

        public Stream Render(Stream body)
        {
            return body;
        }

        public IRendering<Stream> With(string name, string value)
        {
            return this;
        }
    }
}

