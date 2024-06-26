﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Tonga.IO;
using WHyLL.Rendering;
using Xunit;

namespace WHyLL.AspNet.Request.Test
{
    public sealed class FromAspRequestTests
    {
        [Fact]
        public async void ConvertsRequestLine()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/your/shit/together";

            Assert.Equal(
                "GET /your/shit/together HTTP/1.1",
                await new FromAspRequest(
                    httpContext.Request
                ).Render(new FirstLine())
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
                await new FromAspRequest(
                    httpContext.Request
                ).Render(new FirstHeader("check"))
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
                (await new FromAspRequest(
                    httpContext.Request
                ).Render(new AllHeaders()))["check"]
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
                (await new FromAspRequest(
                    httpContext.Request
                ).Render(new BodyAsText()))
            );
        }
    }
}

