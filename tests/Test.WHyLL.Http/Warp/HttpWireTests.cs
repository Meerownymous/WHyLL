﻿using Tonga.Bytes;
using Tonga.IO;
using Tonga.Map;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.Http.Request;
using WHyLL.Http.Warp;
using WHyLL.Warp;
using Xunit;

namespace Test.WHyLL.Http.Warp
{
	public sealed class HttpWireTests
	{
        [Fact]
        public async void ConfiguresRequestUri()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(
                        new RequestLine(
                            "GET",
                            new Uri("http://www.enhanced-calm.com/resource")
                        ).AsString()
                    )
                    .Render();

            Assert.Equal(
                new Uri("http://www.enhanced-calm.com/resource"),
                result.RequestUri
            );
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("PUT")]
        [InlineData("POST")]
        [InlineData("DELETE")]
        [InlineData("HEAD")]
        [InlineData("OPTIONS")]
        [InlineData("TRACE")]
        [InlineData("CONNECT")]
        public async void ConfiguresRequestMethod(string method)
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(
                        new RequestLine(
                            method,
                            new Uri("http://www.enhanced-calm.com/resource")
                        ).AsString()
                    )
                    .Render();

            Assert.Equal(
                result.Method,
                new HttpMethod(method)
            );
        }

        [Fact]
        public async void ConfiguresRequestHttpVersion()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(
                        new RequestLine(
                            "GET",
                            new Uri("http://www.enhanced-calm.com/resource"),
                            new Version(3,0)
                        ).AsString()
                    )
                    .Render();

            Assert.Equal(
                result.Version,
                new Version(3,0)
            );
        }

        [Fact]
        public async void ConfiguresRequestHeader()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                    .Refine(new Header("better", "header"))
                    .Render();

            Assert.Contains(
                result.Headers,
                header => header.Key == "better" && header.Value.Contains("header")
            );
        }

        [Fact]
        public async void ConfiguresRequestContent()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                    .Refine(new MemoryStream(AsBytes._("Me so buried in bytestream").Bytes()))
                    .Render();

            Assert.Equal(
                "Me so buried in bytestream",
                AsText._(result.Content.ReadAsStream()).AsString()
            );
        }
        
        [Fact]
        public async void AddsContentHeaders()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                        message =>
                        {
                            result = message;
                            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                        })
                    .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                    .Refine(new MemoryStream(AsBytes._("Me so buried in bytestream").Bytes()))
                    .Refine(new AsPair<string, string>("Content-type", "text/plain"))
                    .Render();

            Assert.Equal(
                "text/plain",
                AsText._(result.Content.Headers.ContentType.ToString()).AsString()
            );
        }

        [Fact]
        public async void ConvertsResponseStatusLine()
        {
            Assert.Equal(
                "HTTP/1.1 200 OK\r\n",
                await
                    new HttpWire(_ => 
                        Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK))
                    )
                    .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                    .Render()
                    .To(new FirstLine())
            );
        }

        [Fact]
        public async void ConvertsResponseHeaders()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Headers.TryAddWithoutValidation("header", "call Saul");

            Assert.Contains(
                "call Saul",
                (await
                    new HttpWire(_ => Task.FromResult(response))
                        .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                    .Render()
                    .To(new AllHeaders())
                )["header"]
            );
        }

        [Fact]
        public async void PassesResponseBody()
        {
            Assert.Equal(
                "I wanna be read lazyly",
                AsText._(
                    new AsInput(
                        await
                            new HttpWire(_ =>
                                Task.FromResult(
                                    new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                                    {
                                        Content =
                                            new StreamContent(
                                                new MemoryStream(
                                                    AsBytes._("I wanna be read lazyly").Bytes()
                                                )
                                            )
                                    })
                                )
                                .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                                .Render()
                                .To(new Body())
                    )
                ).AsString()
            );
        }

        [Fact]
        public async void DoesNotCopyResponseStreamToMemoryBeforeReading()
        {
            var output = new MemoryStream();

            await
                new HttpWire(
                    _ => Task.FromResult(
                        new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                        {
                            Content =
                                new StreamContent(
                                    new TeeInputStream(
                                        new MemoryStream(
                                            AsBytes._("I wanna be read lazyly").Bytes()
                                        ),
                                        output
                                    )
                                )
                        }
                    )
                )
                .Refine(new RequestLine("GET", new Uri("http://www.enhanced-calm.com/resource")).AsString())
                .Render()
                .To(new Body());

            Assert.Equal(0, output.Length);
        }
    }
}
