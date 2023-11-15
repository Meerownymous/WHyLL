using System;
using Tonga;

namespace Whyre
{
	public interface IRendering<T>
	{
		IRendering<T> Refine(IPair<string,string> part);
		IRendering<T> Refine(Stream body);
		Task<T> Render();
	}
}

