using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Tonga.IO;
using WHyLL.AspNet.Response;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.AspNet.Response
{
    public sealed class UnwrapAspRequestTests
    {
        [Fact]
        public async Task ConvertsResponseLine()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;

            Assert.Equal(
                "HTTP/1.1 200 OK",
                await new UnwrapAspResponse(
                    httpContext.Response
                ).To(new Prologue())
            );
        }

        [Fact]
        public async Task ConvertsHeaders()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;
            httpContext.Response.Headers
                .Add("check", new StringValues("this out"));

            Assert.Equal(
                "this out",
                await new UnwrapAspResponse(
                    httpContext.Response
                ).To(new FirstHeader("check"))
            );
        }

        [Fact]
        public async Task ConvertsMultipleHeadersWithSameKey()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;
            httpContext.Response.Headers
                .Add("check", new StringValues(["this out", "this also"]));

            Assert.Equal(
                ["this out", "this also"],
                (await new UnwrapAspResponse(
                    httpContext.Response
                ).To(new AllHeaders()))["check"]
            );
        }

        [Fact]
        public async Task ConvertsBody()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;
            httpContext.Response.Body = new AsConduit("booody").Stream();

            Assert.Equal(
                "booody",
                await new UnwrapAspResponse(
                    httpContext.Response
                ).To(new BodyAsString())
            );
        }
    }
}

