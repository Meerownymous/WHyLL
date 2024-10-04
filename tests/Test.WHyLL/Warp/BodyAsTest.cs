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
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                "I am the body",
                (await new SimpleMessage()
                    .WithBody(new AsInput("I am the body").Stream())
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

