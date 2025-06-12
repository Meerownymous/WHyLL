using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class SummaryTests
    {
        [Fact]
        public async Task RendersAll()
        {
            var rendered = 0;
            await new SimpleMessage().To(
                new Summary<int>(
                        FromScratch._(
                            () => Task.FromResult(++rendered)
                        ).AsRepeated(3)
                    )
                );
            Assert.Equal(3, rendered);
        }

        [Fact]
        public async Task ThrowsException()
        {
            var rendered = 0;
            await Assert.ThrowsAsync<Exception>(async () =>
                await new SimpleMessage().To(
                    new Summary<int>(
                        (
                            FromScratch._(() => rendered++),
                            FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                            FromScratch._(() => rendered++)
                        ).AsEnumerable()
                    )
                )
            );
        }

        [Fact]
        public async Task StopsAtException()
        {
            var rendered = 0;
            try
            {
                await new SimpleMessage().To(
                    new Summary<int>(
                        (
                            FromScratch._(() => rendered++),
                            FromScratch._<int>(render: () => throw new Exception("HALT STOP")),
                            FromScratch._(() => rendered++)
                        ).AsEnumerable()
                    )
                );
            }
            catch (Exception)
            {
                // ignored
            }

            Assert.Equal(1, rendered);
        }

        [Fact]
        public async void ReturnsLast()
        {
            Assert.Equal(
                "Last",
                await new SimpleMessage().To(
                    new Summary<string>(
                        (
                            FromScratch._(() => "First"),
                            FromScratch._(() => "Last")
                        ).AsEnumerable()
                    )
                )
            );
        }

        [Fact]
        public async Task SummarizesByLambda()
        {
            var result = "";
            await new SimpleMessage().To(
                new Summary<string>(
                    (
                        FromScratch._(() => result += "A"),
                        FromScratch._(() => result += "B")
                    ).AsEnumerable()
                )
            );
            Assert.Equal("AB", result);
        }
    }
}