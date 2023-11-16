using System;
using Tonga;
using Tonga.Collection;
using Tonga.Map;
using Tonga.Text;
using Whyre;
using Whyre.Parts;

namespace Whyre.Test
{
    public sealed class Body : IRendering<Stream>
    {
        private readonly Stream body;

        public Body() : this(new MemoryStream())
        { }

        private Body(Stream body)
        {
            this.body = body;
        }

        public IRendering<Stream> Refine(string firstLine)
        {
            return this;
        }

        public IRendering<Stream> Refine(IPair<string, string> part)
        {
            return this;
        }

        public IRendering<Stream> Refine(Stream body)
        {
            return new Body(body);
        }

        public async Task<Stream> Render()
        {
            return await Task.FromResult(body);
        }
    }
}

