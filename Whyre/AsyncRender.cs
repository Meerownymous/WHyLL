using System;
namespace Whyre
{
	public sealed class MessageRender
	{
        private readonly Task<IMessage> before;

        public MessageRender(Task<IMessage> before)
		{
            this.before = before;
        }

		public async Task<T> Render<T>(IRendering<T> rendering)
		{
			return await (this.before.Result).Render(rendering);
        }
	}
}

