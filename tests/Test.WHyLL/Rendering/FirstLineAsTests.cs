using System;
using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.Rendering;
using Xunit;

namespace Test.WHyLL.Rendering
{
    public sealed class FirstLineAsTest
    {
        [Fact]
        public async void RendersAsOutputType()
        {
            Assert.Equal(
                123,
                (await new SimpleMessage()
                    .With("123")
                    .Render(new FirstLineAs<int>(int.Parse))
                )
            );
        }
    }
}

