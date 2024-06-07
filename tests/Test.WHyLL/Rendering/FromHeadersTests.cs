using WHyLL.Headers;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class FromHeadersTests
    {
        [Fact]
        public async void RendersFromHeaders()
        {
            Assert.Equal(
                "woohoo",
                await
                (
                    new FromHeaders<string>(async headers =>
                        await Task.FromResult(new HeaderMap(headers)["cheer"].ElementAt(0))
                    ).Refine(new Header("cheer", "woohoo"))
                ).Render()
            );
        }
    }
}

