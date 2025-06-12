using WHyLL.Message;
using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Message
{
    public sealed class AsyncMessageTests
    {
        [Fact]
        public async Task UsesAsyncFunction()
        {
            Assert.Equal(
                "BE /lazy lazytp/1.1",
                (await new AsyncMessage(async () =>
                    await new SimpleMessage()
                        .With(new AsPrologue(["BE", "/lazy", "lazytp/1.1"]))
                        .To(new Clone())
                    )
                    .To(new Prologue())
                )
            );
        }
    }
}