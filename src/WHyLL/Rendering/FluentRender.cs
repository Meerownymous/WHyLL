using System;
namespace WHyLL.Rendering
{
    public static class FluentRender
    {
        public static async Task<T> Render<T>(this Task<IMessage> responseTask, IRendering<T> rendering)
        {
            return await (await responseTask).Render(rendering);
        }
    }
}

