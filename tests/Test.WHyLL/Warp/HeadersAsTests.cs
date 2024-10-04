using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
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
                    .To(
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

