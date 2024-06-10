using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Tonga.IO;
using WHyLL.AspNet.Request;
using WHyLL.Rendering;
using Xunit;

namespace WHyLL.AspNet.Response.Test
{
    public sealed class FromAspRequestTests
    {
        [Fact]
        public async void ConvertsResponseLine()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;

            Assert.Equal(
                "HTTP/1.1 200 OK",
                await new FromAspResponse(
                    httpContext.Response
                ).Render(new FirstLine())
            );
        }

        [Fact]
        public async void ConvertsHeaders()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;
            httpContext.Response.Headers
                .Add("check", new StringValues("this out"));

            Assert.Equal(
                "this out",
                await new FromAspResponse(
                    httpContext.Response
                ).Render(new FirstHeader("check"))
            );
        }

        [Fact]
        public async void ConvertsMultipleHeadersWithSameKey()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;
            httpContext.Response.Headers
                .Add("check", new StringValues(new string[] { "this out", "this also" }));

            Assert.Equal(
                new string[] { "this out", "this also" },
                (await new FromAspResponse(
                    httpContext.Response
                ).Render(new AllHeaders()))["check"]
            );
        }

        [Fact]
        public async void ConvertsBody()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.StatusCode = 200;
            httpContext.Response.Body = new AsInput("booody").Stream();

            Assert.Equal(
                "booody",
                (await new FromAspResponse(
                    httpContext.Response
                ).Render(new BodyAsText()))
            );
        }
    }
}

