using Tonga.IO;
using Tonga.Text;
using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
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
                    .Render(
                        new BodyAs<string>(body=>
                            AsText._(body).AsString()
                        )
                    )
                )
            );
        }
    }
}

