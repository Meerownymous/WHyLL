using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
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
                    .Render(new FirstLineAs<int>(int.Parse))
                )
            );
        }
    }
}