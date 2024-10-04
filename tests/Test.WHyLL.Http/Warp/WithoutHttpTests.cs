using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using WHyLL;
using WHyLL.Http.Warp;
using WHyLL.Message;
using WHyLL.Warp;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Http.Warp
{
    public sealed class WithoutHttpTest
    {
        [Fact]
        public async void RemovesHeaders()
        {
            Assert.DoesNotContain(
                "HOST",
                (
                    await new SimpleMessage(
                        "POST /unittest HTTP/1.1",
                        AsEnumerable._(
                            AsPair._("HOST", "localhost"),
                            AsPair._("leave-me", "please")
                        ),
                        new MemoryStream()
                    )
                    .To(new WithoutHttp())
                    .To(new AllHeaders())
                )
                .Keys()
            );
        }

        [Fact]
        public async void PreservesUnmatchedHeaders()
        {
            Assert.Contains(
                "leave-me",
                (
                    await new SimpleMessage(
                        "POST /unittest HTTP/1.1",
                        AsEnumerable._(
                            AsPair._("HOST", "localhost"),
                            AsPair._("leave-me", "please")
                        ),
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
                    "GET /unittest HTTP/1.1",
                    None._<IPair<string, string>>(),
                    new MemoryStream()
                )
                .To(new WithoutHttp())
                .To(new FirstLine())
            );
        }
    }
}

