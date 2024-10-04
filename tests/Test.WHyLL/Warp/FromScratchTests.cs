using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
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

