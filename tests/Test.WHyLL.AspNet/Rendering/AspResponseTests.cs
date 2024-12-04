using System.Net;
using Microsoft.AspNetCore.Http;
using Tonga.IO;
using Tonga.Text;
using WHyLL.AspNet.Warp;
using WHyLL.Headers;
using WHyLL.Http.Response;
using WHyLL.Message;
using WHyLL;
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
                    .To(new AspResponse(new DefaultHttpContext()))
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
                    .To(new AspResponse(new DefaultHttpContext()))
                ).Headers["whoomp"]
            );
        }
        
        [Fact]
        public async void RendersMultipleHeadersWithSameKey()
        {
            Assert.Equal(
                "there it is,there it idelli-diddeli-is",
                (await new SimpleMessage()
                    .With(new ResponseLine(HttpStatusCode.OK).AsString())
                    .With(new Header("whoomp", "there it is"))
                    .With(new Header("whoomp", "there it idelli-diddeli-is"))
                    .To(new AspResponse(new DefaultHttpContext()))
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
                        .To(new AspResponse(context))
                    ).Body
                ).AsString()
            );
        }
    }
}

