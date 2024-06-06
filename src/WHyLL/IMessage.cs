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
        IMessage With(string firstLine);

        /// <summary>
        /// Refine the headers.
        /// </summary>
        IMessage With(params IPair<string, string>[] parts);

        /// <summary>
        /// Refine the headers.
        /// </summary>
        IMessage With(IEnumerable<IPair<string,string>> parts);

        /// <summary>
        /// Refine the body to the request.
        /// </summary>
        IMessage WithBody(Stream body);

        /// <summary>
        /// Render the message to a response, a followup or something else.
        /// </summary>
        Task<T> Render<T>(IRendering<T> rendering);
    }
}