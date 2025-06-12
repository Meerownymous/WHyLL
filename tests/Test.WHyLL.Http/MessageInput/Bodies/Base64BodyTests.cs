using Tonga.Text;
using WHyLL.Http.MessageInput.Bodies;
using WHyLL.Message;
using WHyLL.Warp;
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
                    new MessageWithInputs(
                        new Base64Body("I am 64")    
                    ).To(new BodyAsString())
            ).Str()
        );
    }
}