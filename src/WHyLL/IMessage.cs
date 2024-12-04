using System;
using Tonga;

namespace WHyLL
{
	/// <summary>
	/// HTTP request.
	/// </summary>
	public interface IMessage
	{
        /// <summary>
        /// Refine the first line of the message -
        /// request-line (request message)
        /// or status-line (response message).
        /// </summary>
        IMessage With(string newFirstLine);

        /// <summary>
        /// Refine the headers.
        /// </summary>
        IMessage With(IEnumerable<IPair<string,string>> newParts);

        /// <summary>
        /// Refine the body to the request.
        /// </summary>
        IMessage WithBody(Stream newBody);

        /// <summary>
        /// Render the message to a response, a followup or something else.
        /// </summary>
        Task<T> To<T>(IWarp<T> warp);
    }
}