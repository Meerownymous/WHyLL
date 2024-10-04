using WHyLL.Message;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Warp
{
    public sealed class FixedTests
    {
        [Fact]
        public async void Renders()
        {
            Assert.Equal(
                201051,
                await new SimpleMessage().To(
                    new Fixed<int>(201051)
                )
            );
        }
    }
}

