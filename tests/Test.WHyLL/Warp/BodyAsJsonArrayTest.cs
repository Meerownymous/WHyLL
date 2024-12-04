using Tonga.IO;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class BodyAsJsonArrayTest
    {
        [Fact]
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                "I am the body",
                (await new SimpleMessage()
                    .WithBody(new AsInput("[{ text: 'I am the body'}]").Stream())
                    .To(new BodyAsJsonArray())
                )[0]["text"]
            );
        }
    }
}

