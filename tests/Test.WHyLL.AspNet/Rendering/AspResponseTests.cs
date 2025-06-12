using System.Net;
using Microsoft.AspNetCore.Http;
using Tonga.IO;
using Tonga.Text;
using WHyLL.AspNet.Warp;
using WHyLL.Headers;
using WHyLL.Http.Response;
using WHyLL.Message;
using Xunit;

namespace Test.WHyLL.AspNet.Rendering
{
    public sealed class AspResponseTests
    {
        [Fact]
        public async Task RendersStatus()
        {
            Assert.Equal(
                200,
                (await new SimpleMessage()
                    .With(new ResponsePrologue(HttpStatusCode.OK))
                    .To(new AspResponse(new DefaultHttpContext()))
                ).StatusCode
            );
        }

        [Fact]
        public async Task RendersHeaders()
        {
            Assert.Equal(
                "there it is",
                (await new SimpleMessage()
                    .With(new ResponsePrologue(HttpStatusCode.OK))
                    .With(new Header("whoomp", "there it is"))
                    .To(new AspResponse(new DefaultHttpContext()))
                ).Headers["whoomp"]
            );
        }
        
        [Fact]
        public async Task RendersMultipleHeadersWithSameKey()
        {
            Assert.Equal(
                "there it is,there it idelli-diddeli-is",
                (await new SimpleMessage()
                    .With(new ResponsePrologue(HttpStatusCode.OK))
                    .With(new Header("whoomp", "there it is"))
                    .With(new Header("whoomp", "there it idelli-diddeli-is"))
                    .To(new AspResponse(new DefaultHttpContext()))
                ).Headers["whoomp"]
            );
        }

        [Fact]
        public async Task RendersBody()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            Assert.Equal(
                "Clean Code",
                    (await new SimpleMessage()
                        .With(new ResponsePrologue(200))
                        .WithBody(new AsConduit("Clean Code").Stream())
                        .To(new AspResponse(context))
                    ).Body
                    .AsText()
                    .Str()
            );
        }
    }
}

