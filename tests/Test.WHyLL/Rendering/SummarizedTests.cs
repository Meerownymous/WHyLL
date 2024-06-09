using Tonga.Enumerable;
using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class SummarizedTests
    {
        [Fact]
        public async void RendersAll()
        {
            var rendered = 0;
            await new SimpleMessage().Render(
                    Summarized._(
                        Repeated._(
                            FromScratch._(
                                () => Task.FromResult(++rendered)
                            ),
                            3
                        )
                    )
                );
            Assert.Equal(3, rendered);
        }

        [Fact]
        public async void ThrowsException()
        {
            var rendered = 0;
            await Assert.ThrowsAsync<Exception>(async () =>
                await new SimpleMessage().Render(
                    Summarized._(
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
                    Summarized._(
                        AsEnumerable._(
                            FromScratch._(() => rendered++),
                            FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                            FromScratch._(() => rendered++)
                        )
                    )
                );
            }
            catch(Exception ex){ }

            Assert.Equal(1, rendered);
        }

        [Fact]
        public async void ReturnsLast()
        {
            Assert.Equal(
                "Last",
                await new SimpleMessage().Render(
                    Summarized._(
                        AsEnumerable._(
                            FromScratch._(() => "First"),
                            FromScratch._(() => "Last")
                        )
                    )
                )
            );
        }

        [Fact]
        public async void SummarizesByLambda()
        {
            var result = "";
            await new SimpleMessage().Render(
                Summarized._(
                    AsEnumerable._(
                        FromScratch._(() => result += "A"),
                        FromScratch._(() => result += "B")
                    )
                )
            );
            Assert.Equal("AB", result);
        }
    }
}