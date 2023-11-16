using System;
using Tonga;

namespace Whyre.Rendering
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

        public IRendering<string> Refine(string start)
        {
            return new FirstLine(start);
        }

        public IRendering<string> Refine(IPair<string, string> header)
        {
            return this;
        }

        public IRendering<string> Refine(Stream body)
        {
            return this;
        }

        public Task<string> Render()
        {
            return Task.FromResult(this.firstLine);
        }
    }
}

