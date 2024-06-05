using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class WithoutHeadersTest
    {
        [Fact]
        public async void RemovesHeaders()
        {
            Assert.DoesNotContain(
                "remove-me",
                (
                    await new SimpleMessage(
                        "DO /unittest TEST 0.1",
                        AsEnumerable._(
                            AsPair._("remove-me", "please"),
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
        public async void PreservesUnmatchedHeaders()
        {
            Assert.Contains(
                "leave-me",
                (
                    await new SimpleMessage(
                        "DO /unittest TEST 0.1",
                        AsEnumerable._(
                            AsPair._("remove-me", "please"),
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
    }
}

