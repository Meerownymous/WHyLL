using Tonga.IO;
using Tonga.Text;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class BodyAsJsonTest
    {
        [Fact]
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                "I am the body",
                (await new SimpleMessage()
                    .WithBody(new AsInput("{ text: 'I am the body' }").Stream())
                    .To(new BodyAsJson())
                )["text"]
            );
        }
    }
}

