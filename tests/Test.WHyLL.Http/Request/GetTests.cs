using Tonga;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.Http.Request;
using WHyLL.Warp;
using WHyLL.Message;
using WHyLL.Prologue;
using Xunit;

namespace Test.WHyLL.Http.Request
{
    public sealed class GetTests
    {
        [Fact]
        public async Task RefinesByParts()
        {
            Assert.Equal(
                "successful",
                new First<string>(
                    (await
                        new Get("http://www.enhanced-calm.com", new Version(1, 1))
                            .With(new Header("x-test", "successful"))
                            .To(new AllHeaders())
                    )["x-test"]
                ).Value()
            );
        }

        [Fact]
        public async Task RefinesByRequestLine()
        {
            Assert.Equal(
                "POST http://www.enhanced-calm.com/enhance HTTP/3.0",
                await
                    new Get("http://www.enhanced-calm.com/old", new Version(1, 1))
                        .With(new AsPrologue(["POST", "http://www.enhanced-calm.com/enhance", "HTTP/3.0"]))
                        .To(new Prologue())
                
            );
        }

        [Fact]
        public async Task RefinesByBody()
        {
            Assert.Equal(
                "success",
                await
                    new Get("http://www.enhanced-calm.com", new Version(1, 1))
                        .WithBody("success".AsStream())
                        .To(new Body())
                        .AsText()
                        .Str()
            );
        }
    }
}