using System;
using Tonga;

namespace WHyLL
{
	public interface IWarp<T>
	{
		/// <summary>
		/// Refines the start of the Warp.
		/// </summary>
		IWarp<T> Refine(IPrologue newPrologue);

		/// <summary>
		/// Adds a header to the Warp.
		/// </summary>
		IWarp<T> Refine(params IPair<string,string>[] headers);

		/// <summary>
		/// Adds a header to the Warp.
		/// </summary>
		IWarp<T> Refine(IEnumerable<IPair<string, string>> newParts);

        /// <summary>
        /// Adds a body to the Warp.
        /// </summary>
        IWarp<T> Refine(Stream newBody);

		/// <summary>
		/// Renders the output.
		/// </summary>
		Task<T> Render();
	}
}

