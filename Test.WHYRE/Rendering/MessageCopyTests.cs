using Whyre.Headers;
using Xunit;

namespace Whyre.Rendering.Test
{
    public sealed class MessageCopyTests
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
                    new MessageCopy()
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

