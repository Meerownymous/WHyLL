using System;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace WHyLL.Rendering.Test
{
    public sealed class FromBodyTests
    {
        [Fact]
        public async void RendersFromBody()
        {
            Assert.Equal(
                "boody",
                await
                (
                    new FromBody<string>(
                        async body => await Task.FromResult(AsText._(body).AsString())
                    ).Refine(new AsInput("boody").Stream())
                ).Render()
            );
        }
    }
}

