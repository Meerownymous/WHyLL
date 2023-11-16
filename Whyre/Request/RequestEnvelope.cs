using System;
using Tonga;

namespace Whyre.Request
{
    /// <summary>
    /// Envelope for messages.
    /// </summary>
	public abstract class MessageEnvelope : IMessage
	{
        private readonly IMessage origin;

        /// <summary>
        /// Envelope for messages.
        /// </summary>
        public MessageEnvelope(IMessage origin)
		{
            this.origin = origin;
        }

        public IMessage With(string firstLine)
        {
            return this.origin.With(firstLine);
        }

        public IMessage With(IPair<string, string> parts)
        {
            return this.origin.With(parts);
        }

        public IMessage WithBody(Stream body)
        {
            return this.origin.WithBody(body);
        }

        public Task<T> Render<T>(IRendering<T> rendering)
        {
            return this.origin.Render(rendering);
        }
    }
}

