using System;
using Tonga;

namespace Whyre.Request
{
	public class RequestEnvelope : IRequest
	{
        private readonly IRequest origin;

        public RequestEnvelope(IRequest origin)
		{
            this.origin = origin;
        }

        public Task<T> Render<T>(IRendering<T> rendering)
        {
            return this.origin.Render(rendering);
        }

        public IRequest Refine(Stream body)
        {
            return this.origin.Refine(body);
        }

        public IRequest Refined(IPair<string, string> parts)
        {
            return this.origin.Refined(parts);
        }

        public IRequest Refined(IRequestInput input)
        {
            return this.origin.Refined(input);
        }
    }
}

