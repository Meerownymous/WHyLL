using Tonga.Bytes;
using Tonga.Scalar;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.Http.Request;
using WHyLL.Warp;
using WHyLL.Message;
using Xunit;

namespace Test.WHyLL.Http.Request
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
                        new Get("http://www.enhanced-calm.com", new Version(1, 1))
                            .With(new Header("x-test", "successful"))
                            .To(new AllHeaders())
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
                    new Get("http://www.enhanced-calm.com/old", new Version(1, 1))
                        .With("POST http://www.enhanced-calm.com/enhance HTTP/3.0")
                        .To(new FirstLine())
                
            );
        }

        [Fact]
        public async void RefinesByBody()
        {
            Assert.Equal(
                "success",
                AsText._(
                    await
                        new Get("http://www.enhanced-calm.com", new Version(1, 1))
                            .WithBody(new MemoryStream(AsBytes._("success").Bytes()))
                            .To(new Body())
                ).AsString()
            );
        }
    }
}