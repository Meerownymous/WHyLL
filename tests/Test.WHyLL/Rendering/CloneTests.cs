using WHyLL.Headers;
using Xunit;

namespace WHyLL.Rendering.Test
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
                        .Render(new AllHeaders())
                )[key]
            );
        }
    }
}

