using Tonga.Text;
using WHyLL.Http.MessageInput.Bodies;
using WHyLL.Message;
using WHyLL.Rendering;
using Xunit;

namespace Test.WHyLL.Http.MessageInput.Bodies;

public sealed class Base64BodyTests
{
    [Fact]
    public async void EncodesBody()
    {
        Assert.Equal(
            "I am 64",
            new Base64Decoded(
                await
                    new MessageOfInputs(
                        new Base64Body("I am 64")    
                    ).Render(new BodyAsText())
            ).AsString()
        );
    }
}