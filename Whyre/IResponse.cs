using System;
using Tonga;

namespace Whyre
{
	/// <summary>
	/// Response resulting from request.
	/// </summary>
	public interface IResponse
    {
		IResponse With(string name, string value);
		IResponse With(Stream body);
        T Render<T>(IRendering<T> rendering); 
	}
}