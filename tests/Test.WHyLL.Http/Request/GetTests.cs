using Tonga.Bytes;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;
using WHyLL.Rendering;
using WHyLL.Headers;

namespace WHyLL.Http.Request.Test
{
    public sealed class GetTests
    {
        [Fact]
        public async void RefinesByParts()
        {
            Assert.Equal(
                "successful",
                First._(
                    (await
                        new Get(new Uri("http://www.enhanced-calm.com"), new Version(1, 1))
                            .With(new Header("x-test", "successful"))
                            .Render(new AllHeaders())
                    )["x-test"]
                ).Value()
            );
        }

        [Fact]
        public async void RefinesByRequestLine()
        {
            Assert.Equal(
                "POST http://www.enhanced-calm.com/enhance HTTP/3.0",
                await
                    new Get(new Uri("http://www.enhanced-calm.com/old"), new Version(1, 1))
                        .With("POST http://www.enhanced-calm.com/enhance HTTP/3.0")
                        .Render(new FirstLine())
                
            );
        }

        [Fact]
        public async void RefinesByBody()
        {
            Assert.Equal(
                "success",
                AsText._(
                    await
                        new Get(new Uri("http://www.enhanced-calm.com"), new Version(1, 1))
                            .WithBody(new MemoryStream(AsBytes._("success").Bytes()))
                            .Render(new Body())
                ).AsString()
            );
        }
    }
}