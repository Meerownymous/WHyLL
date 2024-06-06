using Tonga;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Render the first line of a request (request-line) or response (status-line)
    /// </summary>
	public sealed class FirstLine : IRendering<string>
	{
        private readonly string firstLine;

        /// <summary>
        /// Render the first line of a request (request-line) or response (status-line)
        /// </summary>
        public FirstLine() : this(string.Empty)
        { }

        private FirstLine(string firstLine)
        {
            this.firstLine = firstLine;
        }

        public IRendering<string> Refine(string start) => new FirstLine(start);
        public IRendering<string> Refine(params IPair<string, string>[] header) => this;
        public IRendering<string> Refine(IEnumerable<IPair<string, string>> header) => this;
        public IRendering<string> Refine(Stream body) => this;

        public Task<string> Render()
        {
            return Task.FromResult(this.firstLine);
        }
    }
}

