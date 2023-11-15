using System;
using Tonga;

namespace Whyre.Request
{
	public class RequestEnvelope : IMessage
	{
        private readonly IMessage origin;

        public RequestEnvelope(IMessage origin)
		{
            this.origin = origin;
        }

        public Task<T> Render<T>(IRendering<T> rendering)
        {
            return this.origin.Render(rendering);
        }

        public IMessage Refine(Stream body)
        {
            return this.origin.Refine(body);
        }

        public IMessage Refined(IPair<string, string> parts)
        {
            return this.origin.Refined(parts);
        }

        //public IMessage Refined(IMessageInput input)
        //{
        //    return this.origin.Refined(input);
        //}
    }
}

