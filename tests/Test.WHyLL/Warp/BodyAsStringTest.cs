using Tonga.IO;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class BodyAsStringTest
    {
        [Fact]
        public async Task RendersAsText()
        {
            Assert.Equal(
                "I am the body",
                await new SimpleMessage()
                    .WithBody(new AsInput("I am the body").Stream())
                    .To(
                        new BodyAsString()
                    )
            );
        }
        
        [Fact]
        public async Task RendersUTF8VariableLengthChars()
        {
            Assert.Equal(
                "I am the bödy",
                await new SimpleMessage()
                    .WithBody(new AsInput("I am the bödy").Stream())
                    .To(
                        new BodyAsString()
                    )
            );
        }
        
        [Fact]
        public async Task ResetsStreamAfterRender()
        {
            var body = new AsInput("I am the bödy").Stream();
            body.Seek(9, SeekOrigin.Begin);

            await new SimpleMessage()
                .WithBody(body)
                .To(new BodyAsString());
            Assert.Equal(
                "bödy",
                new StreamReader(body).ReadToEnd()
            );
        }
        
        [Fact]
        public async Task RendersFullyWhenPositionNotZero()
        {
            var body = new AsInput("I am the bödy").Stream();
            body.Seek(9, SeekOrigin.Begin);

            
            Assert.Equal(
                "I am the bödy",
                await new SimpleMessage()
                    .WithBody(body)
                    .To(new BodyAsString())
            );
        }
    }
}

