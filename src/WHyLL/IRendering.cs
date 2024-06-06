using System;
using Tonga;

namespace WHyLL
{
	public interface IRendering<T>
	{
		/// <summary>
		/// Refines the start of the rendering.
		/// </summary>
		IRendering<T> Refine(string start);

		/// <summary>
		/// Adds a header to the rendering.
		/// </summary>
		IRendering<T> Refine(params IPair<string,string>[] headers);

		/// <summary>
		/// Adds a header to the rendering.
		/// </summary>
		IRendering<T> Refine(IEnumerable<IPair<string, string>> headers);

        /// <summary>
        /// Adds a body to the rendering.
        /// </summary>
        IRendering<T> Refine(Stream body);

		/// <summary>
		/// Renders the output.
		/// </summary>
		Task<T> Render();
	}
}

