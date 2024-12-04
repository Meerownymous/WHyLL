using Tonga;

namespace WHyLL.Message
{
    /// <summary>
    /// Envelope for messages.
    /// </summary>
	public abstract class MessageEnvelope(IMessage origin) : IMessage
	{
        public IMessage With(string firstLine) =>
            origin.With(firstLine);

        public IMessage With(IEnumerable<IPair<string, string>> newParts) =>
            origin.With(newParts);

        public IMessage WithBody(Stream newBody) =>
            origin.WithBody(newBody);

        public Task<T> To<T>(IWarp<T> warp) =>
            origin.To(warp);
    }
}

