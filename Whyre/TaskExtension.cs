using System;
using System.Xml;

namespace Whyre
{
	public static class TaskExtension
	{
        public static async Task<T> Render<T>(this Task<IMessage> entityTask, IRendering<T> rendering)
        {
            return await entityTask.Render(rendering);
        }
    }
}

