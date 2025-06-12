using WHyLL.Message;
using WHyLL.Prologue;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class PrologueAsTest
    {
        [Fact]
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                123,
                await new SimpleMessage()
                    .With(new AsPrologue(["123"]))
                    .To(new PrologueAs<int>(prologue => int.Parse(prologue.Sequence()[0])))
            );
        }
    }
}