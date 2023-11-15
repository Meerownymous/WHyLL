using System;
using Tonga;

namespace Whyre
{
	/// <summary>
	/// HTTP request.
	/// </summary>
	public interface IMessage
	{
        /// <summary>
        /// Refine the request.
        /// </summary>
        IMessage Refined(IPair<string,string> parts);

        ///// <summary>
        ///// Refine the request.
        ///// </summary>
        //IMessage Refined(IMessageInput input);

        /// <summary>
        /// Append a body to the request.
        /// </summary>
        IMessage Refine(Stream body);

        /// <summary>
        /// 
        /// </summary>
        Task<T> Render<T>(IRendering<T> rendering);
	}
}