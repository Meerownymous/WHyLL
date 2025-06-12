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
        public async Task RendersAsOutputType()
        {
            Assert.Equal(
                ["A", "B"],
                await new SimpleMessage()
                    .With(("A", "some value").AsPair())
                    .With(("B", "some other value").AsPair())
                    .To(
                        new HeadersAs<IEnumerable<string>>(headers =>
                            headers.AsMapped(header => header.Key())
                        )
                    )
                
            );
        }
    }
}

