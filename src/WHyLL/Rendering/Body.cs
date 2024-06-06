using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the body of a message as <see cref="Stream"/>
    /// </summary>
    public sealed class Body : IRendering<Stream>
    {
        private readonly Stream body;

        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        public Body() : this(new MemoryStream())
        { }

        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        private Body(Stream body)
        {
            this.body = body;
        }

        public IRendering<Stream> Refine(string firstLine)
        {
            return this;
        }

        public IRendering<Stream> Refine(IEnumerable<IPair<string, string>> parts) => this;
        public IRendering<Stream> Refine(IPair<string, string>[] parts) => this;

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

