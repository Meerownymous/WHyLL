using Tonga.IO;
using Tonga.Text;
using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class BodyAsTest
    {
        [Fact]
        public async Task RendersAsOutputType()
        {
            Assert.Equal(
                "I am the body",
                (await new SimpleMessage()
                    .WithBody(new AsConduit("I am the body").Stream())
                    .To(
                        new BodyAs<string>(body=>
                            AsText._(body).AsString()
                        )
                    )
                )
            );
        }
        
        [Fact]
        public async Task SeeksStreamOrigin()
        {
            var stream = new AsConduit("I am the body").Stream();
            stream.Seek(4, SeekOrigin.Begin);
            Assert.Equal(
                "I am the body",
                (await new SimpleMessage()
                    .WithBody(stream)
                    .To(
                        new BodyAs<string>(body=>
                            AsText._(body).AsString()
                        )
                    )
                )
            );
        }
    }
}

