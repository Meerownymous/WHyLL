using System;
using Tonga;

namespace WHyLL
{
	/// <summary>
	/// HTTP request.
	/// </summary>
	public interface IByteMessage
	{
        /// <summary>
        /// Refine the first line of the message -
        /// request-line (request message)
        /// or status-line (response message).
        /// </summary>
        IByteMessage With(byte[] head);
        
        /// <summary>
        /// Refine the headers.
        /// </summary>
        IByteMessage With(IEnumerable<IPair<string,byte[]>> attributes);

        /// <summary>
        /// Refine the body to the request.
        /// </summary>
        IByteMessage WithBody(Stream newBody);

        /// <summary>
        /// Render the message to a response, a followup or something else.
        /// </summary>
        Task<T> Render<T>(IByteWarp<T> Warp);
    }
}