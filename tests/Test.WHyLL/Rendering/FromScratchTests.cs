using WHyLL.Message;
using Xunit;

namespace WHyLL.Warp.Test
{
    public sealed class FromScratchTests
    {
        [Fact]
        public async void Renders()
        {
            Assert.Equal(
                201051,
                await new SimpleMessage().To(
                    new FromScratch<int>(() => 201051)
                )
            );
        }
    }
}

