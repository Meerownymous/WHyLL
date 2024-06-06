using Tonga;
using Tonga.IO;
using Tonga.Text;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the body of a message as <see cref="String"/>
    /// </summary>
    public sealed class BodyAsText : IRendering<String>
    {
        private readonly Stream body;

        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        public BodyAsText() : this(new MemoryStream())
        { }

        /// <summary>
        /// Renders the body of a message as <see cref="Stream"/>
        /// </summary>
        private BodyAsText(Stream body)
        {
            this.body = body;
        }

        public IRendering<string> Refine(string firstLine) => this;
        public IRendering<string> Refine(IEnumerable<IPair<string, string>> parts) => this;
        public IRendering<string> Refine(IPair<string, string>[] parts) => this;

        public IRendering<string> Refine(Stream body) => new BodyAsText(body);

        public async Task<string> Render()
        {
            return await Task.FromResult(AsText._(body).AsString());
        }
    }
}

