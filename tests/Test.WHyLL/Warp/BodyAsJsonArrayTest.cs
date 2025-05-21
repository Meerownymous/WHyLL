using Tonga.IO;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class BodyAsJsonArrayTest
    {
        [Fact]
        public async Task RendersAsOutputType()
        {
            Assert.Equal(
                "I am the body",
                (await new SimpleMessage()
                    .WithBody(new AsConduit("[{ text: 'I am the body'}]").Stream())
                    .To(new BodyAsJsonArray())
                )[0]["text"]
            );
        }
    }
}

