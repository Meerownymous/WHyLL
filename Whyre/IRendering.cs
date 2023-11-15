using System;
namespace Whyre
{
	public interface IRendering<T>
	{
		IRendering<T> With(string name, string value);
		T Render(Stream body);
	}
}

