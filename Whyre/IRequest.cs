using System;
using Tonga;

namespace Whyre
{
	/// <summary>
	/// HTTP request.
	/// </summary>
	public interface IRequest
	{
        /// <summary>
        /// Refine the request.
        /// </summary>
        IRequest Refined(IPair<string,string> parts);

        /// <summary>
        /// Refine the request.
        /// </summary>
        IRequest Refined(IRequestInput input);

        /// <summary>
        /// Append a body to the request.
        /// </summary>
        IRequest Refine(Stream body);

        /// <summary>
        /// 
        /// </summary>
        Task<T> Render<T>(IRendering<T> rendering);
	}
}