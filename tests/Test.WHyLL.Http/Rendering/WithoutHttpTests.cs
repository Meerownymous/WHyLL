using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Rendering;
using WHyLL.Rendering.Http;
using Xunit;

namespace WHyLL.Message.Test
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
                    .Render(new WithoutHttp())
                    .Render(new AllHeaders())
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
                    .Render(new WithoutHeaders(header => header.Key() == "remove-me"))
                    .Render(new AllHeaders())
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
                .Render(new WithoutHttp())
                .Render(new FirstLine())
            );
        }
    }
}

