using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class HeadersAsTest
    {
        [Fact]
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                new string[] { "A", "B" },
                (await new SimpleMessage()
                    .With(AsPair._("A", "some value"))
                    .With(AsPair._("B", "some other value"))
                    .Render(
                        new HeadersAs<IEnumerable<string>>(headers =>
                            Mapped._(
                                header => header.Key(),
                                headers
                            )
                        )
                    )
                )
            );
        }
    }
}

