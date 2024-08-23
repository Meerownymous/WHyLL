using System.Net;
using Microsoft.AspNetCore.Http;
using Tonga.IO;
using Tonga.Text;
using WHyLL.AspNet.Rendering;
using WHyLL.Headers;
using WHyLL.Http.Response;
using WHyLL.Message;
using Xunit;

namespace Test.WHyLL.AspNet.Rendering
{
    public sealed class AsAspResponseTests
    {
        [Fact]
        public async void RendersStatus()
        {
            Assert.Equal(
                200,
                (await new SimpleMessage()
                    .With(new ResponseLine(HttpStatusCode.OK).AsString())
                    .Render(new AsAspResponse(new DefaultHttpContext()))
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
                    .Render(new AsAspResponse(new DefaultHttpContext()))
                ).Headers["whoomp"]
            );
        }

        [Fact]
        public async void RendersBody()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
                
                
            Assert.Equal(
                "Clean Code",
                AsText._(
                    (await new SimpleMessage()
                        .With(new ResponseLine(200).AsString())
                        .WithBody(new AsInput("Clean Code").Stream())
                        .Render(new AsAspResponse(context))
                    ).Body
                ).AsString()
            );
        }
    }
}

