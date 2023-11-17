using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.Request;
using Xunit;

namespace WHyLL.Rendering.Test
{
	public sealed class AspNetResponseTests
	{
        [Fact]
        public async void ConfiguresRequestUri()
        {
            HttpRequestMessage result = new HttpRequestMessage();
            await
                new AspNetResponse(
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
                result.RequestUri,
                new Uri("http://www.enhanced-calm.com/resource")
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
                new AspNetResponse(
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
                new AspNetResponse(
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
                new AspNetResponse(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
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
                new AspNetResponse(
                    message =>
                    {
                        result = message;
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
                    })
                    .Refine(new MemoryStream(AsBytes._("Me so buried in bytestream").Bytes()))
                    .Render();

            Assert.Equal(
                "Me so buried in bytestream",
                AsText._(result.Content.ReadAsStream()).AsString()
            );
        }

        [Fact]
        public async void ConvertsResponseStatusLine()
        {
            Assert.Equal(
                "HTTP/1.1 200 OK\r\n",
                await
                    new AspNetResponse(
                        new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    )
                    .Render()
                    .Render(new FirstLine())
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
                    new AspNetResponse(response)
                    .Render()
                    .Render(new AllHeaders())
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
                            new AspNetResponse(
                                new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                                {
                                    Content =
                                        new StreamContent(
                                            new MemoryStream(
                                                AsBytes._("I wanna be read lazyly").Bytes()
                                            )
                                        )
                                })
                                .Render()
                                .Render(new Body())
                    )
                ).AsString()
            );
        }

        [Fact]
        public async void DoesNotCopyResponseStreamToMemoryBeforeReading()
        {
            var output = new MemoryStream();

            await
                new AspNetResponse(
                    message => Task.FromResult(
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
                .Render()
                .Render(new Body());

            Assert.Equal(0, output.Length);
        }
    }
}

