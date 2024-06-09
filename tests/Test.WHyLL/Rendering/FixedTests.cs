using WHyLL.Message;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class FixedTests
    {
        [Fact]
        public async void Renders()
        {
            Assert.Equal(
                201051,
                await new SimpleMessage().Render(
                    new Fixed<int>(201051)
                )
            );
        }
    }
}

