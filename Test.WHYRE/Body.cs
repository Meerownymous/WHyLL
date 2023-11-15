using System;
using Tonga;
using Tonga.Collection;
using Tonga.Map;
using Tonga.Text;
using Whyre;
using Whyre.Parts;

namespace WHYRE.Test
{
    public sealed class Body : IRendering<Stream>
    {
        private readonly Stream body;

        public Body() : this(new MemoryStream())
        { }

        public Body(Stream body)
        {
            this.body = body;
        }

        public Stream Render()
        {
            return body;
        }

        public IRendering<Stream> Refine(Stream body)
        {
            return new Body(body);
        }

        public IRendering<Stream> Refine(IPair<string,string> part)
        {
            return this;
        }

        Task<Stream> IRendering<Stream>.Render()
        {
            return Task.FromResult(this.body);
        }
    }
}

