using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Http.Warp;
using WHyLL.Message;
using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Http.Warp
{
    public sealed class WithoutHttpTest
    {
        [Fact]
        public async Task RemovesHeaders()
        {
            Assert.DoesNotContain(
                "HOST",
                (
                    await new SimpleMessage(
                        new AsPrologue(["POST","/unittest","HTTP/1.1"]),
                        (
                            ("HOST", "localhost").AsPair(),
                            ("leave-me", "please").AsPair()
                        ).AsEnumerable(),
                        new MemoryStream()
                    )
                    .To(new WithoutHttp())
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
                        new AsPrologue(["POST","/unittest","HTTP/1.1"]),
                        (
                            ("HOST", "localhost").AsPair(),
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
        public async void ClearsRequestLine()
        {
            Assert.Empty(
                await new SimpleMessage(
                    new AsPrologue(["GET","/unittest","HTTP/1.1"]),
                    new None<IPair<string,string>>(),
                    new MemoryStream()
                )
                .To(new WithoutHttp())
                .To(new Prologue())
            );
        }
    }
}

