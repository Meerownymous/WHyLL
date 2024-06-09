using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class FromScratchTests
    {
        [Fact]
        public async void Renders()
        {
            Assert.Equal(
                201051,
                await new SimpleMessage().Render(
                    new FromScratch<int>(() => 201051)
                )
            );
        }
    }
}

