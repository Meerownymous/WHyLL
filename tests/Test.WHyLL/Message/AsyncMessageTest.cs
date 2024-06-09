using WHyLL.Rendering;
using Xunit;

namespace WHyLL.Message.Test
{
    public sealed class AsyncMessageTests
    {
        [Fact]
        public async void UsesAsyncFunction()
        {
            Assert.Equal(
                "BE /lazy lazytp/1.1",
                (await new AsyncMessage(async () =>
                    await new SimpleMessage()
                        .With("BE /lazy lazytp/1.1")
                        .Render(new Clone())
                    )
                    .Render(new FirstLine())
                )
            );
        }
    }
}