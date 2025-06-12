using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Message;
using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class WithoutHeadersTest
    {
        [Fact]
        public async Task RemovesHeaders()
        {
            Assert.DoesNotContain(
                "remove-me",
                (
                    await new SimpleMessage(
                        new AsPrologue(["DO","/unittest", "TEST", "0.1"]),
                        (
                            ("remove-me", "please").AsPair(),
                            ("leave-me", "please").AsPair()
                        ).AsEnumerable(),
                        new MemoryStream()
                    )
                    .To(new WithoutHeaders(header => header.Key() == "remove-me"))
                    .To(new AllHeaders())
                )
                .Keys()
            );
        }

        [Fact]
        public async Task PreservesUnmatchedHeaders()
        {
            Assert.Contains(
                "leave-me",
                (
                    await new SimpleMessage(
                            new AsPrologue(["DO","/unittest", "TEST", "0.1"]),
                        (
                            ("remove-me", "please").AsPair(),
                            ("leave-me", "please").AsPair()
                        ).AsEnumerable(),
                        new MemoryStream()
                    )
                    .To(new WithoutHeaders(header => header.Key() == "remove-me"))
                    .To(new AllHeaders())
                )
                .Keys()
            );
        }
    }
}

