using System.Net;
using Tonga.IO;
using Tonga.Text;
using WHyLL.AspNet.Rendering;
using WHyLL.Headers;
using WHyLL.Http.Response;
using WHyLL.Message;
using Xunit;

namespace Test.WHyLL.AspNet.Rendering
{
    public sealed class AspResponseTests
    {
        [Fact]
        public async void RendersStatus()
        {
            Assert.Equal(
                200,
                (await new SimpleMessage()
                    .With(new ResponseLine(HttpStatusCode.OK).AsString())
                    .Render(new AspResponse())
                ).StatusCode
            );
        }

        [Fact]
        public async void RendersHeaders()
        {
            Assert.Equal(
                "there it is",
                (await new SimpleMessage()
                    .With(new ResponseLine(HttpStatusCode.OK).AsString())
                    .With(new Header("whoomp", "there it is"))
                    .Render(new AspResponse())
                ).Headers["whoomp"]
            );
        }

        [Fact]
        public async void RendersBody()
        {
            Assert.Equal(
                "Clean Code",
                AsText._(
                    (await new SimpleMessage()
                        .With(new ResponseLine(200).AsString())
                        .WithBody(new AsInput("Clean Code").Stream())
                        .Render(new AspResponse())
                    ).Body
                ).AsString()
            );
        }
    }
}

