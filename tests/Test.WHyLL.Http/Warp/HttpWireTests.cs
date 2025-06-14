using Tonga.Bytes;
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
        public async Task ConfiguresRequestUri()
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
                        new RequestPrologue(
                            "GET",
                            "http://www.enhanced-calm.com/resource"
                        )
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
        public async Task ConfiguresRequestMethod(string method)
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
                        new RequestPrologue(
                            method,
                            "http://www.enhanced-calm.com/resource"
                        )
                    )
                    .Render();

            Assert.Equal(
                result.Method,
                new HttpMethod(method)
            );
        }

        [Fact]
        public async Task ConfiguresRequestHttpVersion()
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
                        new RequestPrologue(
                            "GET",
                            "http://www.enhanced-calm.com/resource",
                            new Version(3,0)
                        )
                    )
                    .Render();

            Assert.Equal(
                result.Version,
                new Version(3,0)
            );
        }

        [Fact]
        public async Task ConfiguresRequestHeader()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                    .Refine(new Header("better", "header"))
                    .Render();

            Assert.Contains(
                result.Headers,
                header => header.Key == "better" && header.Value.Contains("header")
            );
        }

        [Fact]
        public async Task ConfiguresRequestContent()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                    .Refine("Me so buried in bytestream".AsStream())
                    .Render();

            Assert.Equal(
                "Me so buried in bytestream",
                (await result.Content.ReadAsStreamAsync()).AsText().Str()
            );
        }
        
        [Fact]
        public async Task AddsContentHeaders()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new HttpWire(
                        message =>
                        {
                            result = message;
                            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                        })
                    .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                    .Refine("Me so buried in bytestream".AsStream())
                    .Refine(new AsPair<string, string>("Content-type", "text/plain"))
                    .Render();

            Assert.Equal(
                "text/plain",
                result.Content.Headers.ContentType.ToString()
            );
        }

        [Fact]
        public async Task ConvertsResponseStatusLine()
        {
            Assert.Equal(
                "HTTP/1.1 200 OK",
                await
                    new HttpWire(_ => 
                        Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK))
                    )
                    .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                    .Render()
                    .To(new Prologue())
            );
        }

        [Fact]
        public async Task ConvertsResponseHeaders()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Headers.TryAddWithoutValidation("header", "call Saul");

            Assert.Contains(
                "call Saul",
                (await
                    new HttpWire(_ => Task.FromResult(response))
                        .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                    .Render()
                    .To(new AllHeaders())
                )["header"]
            );
        }

        [Fact]
        public async Task PassesResponseBody()
        {
            Assert.Equal(
                "I wanna be read lazyly",
                    new AsConduit(
                        await
                            new HttpWire(_ =>
                                Task.FromResult(
                                    new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                                    {
                                        Content =
                                            new StreamContent(
                                                "I wanna be read lazyly".AsStream()
                                            )
                                    })
                                )
                                .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                                .Render()
                                .To(new Body())
                    ).AsText()
                    .Str()
            );
        }

        [Fact]
        public async Task DoesNotCopyResponseStreamToMemoryBeforeReading()
        {
            var output = new MemoryStream();

            await
                new HttpWire(
                    _ => Task.FromResult(
                        new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                        {
                            Content =
                                new StreamContent(
                                    new TeeOnReadStream(
                                        "I wanna be read lazyly".AsStream(),
                                        output
                                    )
                                )
                        }
                    )
                )
                .Refine(new RequestPrologue("GET", "http://www.enhanced-calm.com/resource"))
                .Render()
                .To(new Body());

            Assert.Equal(0, output.Length);
        }
    }
}

