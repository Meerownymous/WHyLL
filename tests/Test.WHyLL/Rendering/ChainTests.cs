using Tonga.Enumerable;
using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class ChainTests
    {
        [Fact]
        public async void RendersAll()
        {
            var rendered = 0;
            Assert.Equal(
                new[] { 1, 2, 3 },
                await new SimpleMessage().Render(
                    Chain._(
                        Repeated._(
                            FromScratch._(
                                () => Task.FromResult(++rendered)
                            ),
                            3
                        )
                    )
                )
            );
        }

        [Fact]
        public async void ThrowsException()
        {
            var rendered = 0;
            await Assert.ThrowsAsync<Exception>(async () =>
                await new SimpleMessage().Render(
                    Chain._(
                        AsEnumerable._(
                            FromScratch._(() => rendered++),
                            FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                            FromScratch._(() => rendered++)
                        )
                    )
                )
            );
        }

        [Fact]
        public async void StopsAtException()
        {
            var rendered = 0;
            try
            {
                await new SimpleMessage().Render(
                    Chain._(
                        AsEnumerable._(
                            FromScratch._(() => rendered++),
                            FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                            FromScratch._(() => rendered++)
                        )
                    )
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

