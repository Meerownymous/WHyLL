using Tonga;

namespace WHyLL.Message
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

        public IMessage With(string firstLine) =>
            this.origin.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> parts) =>
            this.With(parts.ToArray());

        public IMessage With(params IPair<string, string>[] parts) =>
            this.origin.With(parts);

        public IMessage WithBody(Stream body) =>
            this.origin.WithBody(body);

        public Task<T> Render<T>(IRendering<T> rendering) =>
            this.origin.Render(rendering);
    }
}

