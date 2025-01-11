using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Tonga.IO;
using WHyLL.AspNet.Request;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.AspNet.Request
{
    public sealed class UnwrapAspRequestTests
    {
        [Fact]
        public async void ConvertsRequestLine()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/your/shit/together";

            Assert.Equal(
                "GET /your/shit/together HTTP/1.1",
                await new UnwrapAspRequest(
                    httpContext.Request
                ).To(new FirstLine())
            );  
        }

        [Fact]
        public async void ConvertsHeaders()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/your/shit/together";
            httpContext.Request.Headers
                .Add("check", new StringValues("this out"));

            Assert.Equal(
                "this out",
                await new UnwrapAspRequest(
                    httpContext.Request
                ).To(new FirstHeader("check"))
            );
        }

        [Fact]
        public async void ConvertsMultipleHeadersWithSameKey()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/your/shit/together";
            httpContext.Request.Headers
                .Add("check", new StringValues(new string[] { "this out", "this also" }));

            Assert.Equal(
                new string[] { "this out", "this also" },
                (await new UnwrapAspRequest(
                    httpContext.Request
                ).To(new AllHeaders()))["check"]
            );
        }

        [Fact]
        public async void ConvertsBody()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/your/shit/together";
            httpContext.Request.Body = new AsInput("booody").Stream();

            Assert.Equal(
                "booody",
                (await new UnwrapAspRequest(
                    httpContext.Request
                ).To(new BodyAsString()))
            );
        }
        
        [Fact]
        public async void AllowsBodyReplay()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/your/shit/together";
            httpContext.Request.Body = new AsInput("booody").Stream();

            var msg = new UnwrapAspRequest(httpContext.Request, allowBodyReplay: true);
            await msg.To(new BodyAsString());
            Assert.Equal(
                "booody",
                (await msg.To(new BodyAsString()))
                .ReplaceLineEndings()
            );
        }
    }
}

