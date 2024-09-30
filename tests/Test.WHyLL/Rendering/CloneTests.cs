using WHyLL.Headers;
using WHyLL.Warp;
using Xunit;

namespace WHyLL.Warp.Test
{
    public sealed class CloneTests
    {
        [Theory]
        [InlineData("Test-Type", "Unit")]
        [InlineData("Test-Difficulty", "Easy")]
        [InlineData("Test-Difficulty", "Peasy")]
        public async void RendersHeaders(string key, string value)
        {
            Assert.Contains(
                value,
                (await
                    new Clone()
                        .Refine(new Header("Test-Type", "Unit"))
                        .Refine(new Header("Test-Difficulty", "Easy"))
                        .Refine(new Header("Test-Difficulty", "Peasy"))
                        .Render()
                        .To(new AllHeaders())
                )[key]
            );
        }
    }
}

