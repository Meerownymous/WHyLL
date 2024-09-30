using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using WHyLL.Http.Warp;
using WHyLL.Warp;
using HttpResponse = Microsoft.AspNetCore.Http.HttpResponse;

namespace WHyLL.AspNet.Warp
{
    /// <summary>
    /// Renders a message as AspNetResponse into a HttpContext.
    /// </summary>
    public sealed class AspResponse : WarpEnvelope<HttpResponse>
    {
        public AspResponse(HttpContext context) : base
        (
            (new MessageAs<HttpResponse>(async msg =>
                {
                    context.Response.StatusCode = await msg.To(new StatusCode());
                    var headers = await msg.To(new AllHeaders());

                    foreach (var headerName in headers.Keys())
                    {
                        context.Response.Headers
                            .Add(
                                headerName,
                                new StringValues(headers[headerName].ToArray())
                            );
                    }

                    await (await msg.To(new Body())).CopyToAsync(context.Response.Body);
                    if (context.Response.Body.CanSeek)
                        context.Response.Body.Seek(0, SeekOrigin.Begin);
                    return context.Response;
                })
            )
        )
        { }
        
    }
}