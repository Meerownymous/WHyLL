using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class FirstLineAsTest
    {
        [Fact]
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                123,
                (await new SimpleMessage()
                    .With("123")
                    .To(new FirstLineAs<int>(int.Parse))
                )
            );
        }
    }
}