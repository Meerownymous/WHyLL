using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class ChainTests
    {
        [Fact]
        public async Task RendersAll()
        {
            var rendered = 0;
            Assert.Equal(
                [1, 2, 3],
                await new SimpleMessage()
                    .Chain(
                        FromScratch._(
                            () => Task.FromResult(++rendered)
                        ).AsRepeated(3)
                    )
            );
        }

        [Fact]
        public async Task ThrowsException()
        {
            var rendered = 0;
            await Assert.ThrowsAsync<Exception>(async () =>
                await new SimpleMessage()
                    .Chain(
                        FromScratch._(() => rendered++),
                        FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                        FromScratch._(() => rendered++)
                    )
            );
        }

        [Fact]
        public async Task StopsAtException()
        {
            var rendered = 0;
            try
            {
                await new SimpleMessage()
                    .Chain(
                        FromScratch._(() => rendered++),
                        FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                        FromScratch._(() => rendered++)
                    );
            }
            catch (Exception)
            {
                // ignored
            }
            Assert.Equal(1, rendered);
        }
    }
}

