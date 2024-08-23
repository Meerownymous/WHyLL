using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using WHyLL.Http.Rendering;
using WHyLL.Rendering;
using HttpResponse = Microsoft.AspNetCore.Http.HttpResponse;

namespace WHyLL.AspNet.Rendering
{
    /// <summary>
    /// Renders a message as AspNetResponse into a HttpContext.
    /// </summary>
    public sealed class AsAspResponse : RenderingEnvelope<HttpResponse>
    {
        public AsAspResponse(HttpContext context) : base
        (
            (new MessageAs<HttpResponse>(async msg =>
                {
                    context.Response.StatusCode = await msg.Render(new StatusCode());
                    var headers = await msg.Render(new AllHeaders());

                    foreach (var headerName in headers.Keys())
                        context.Response.Headers
                            .Add(
                                headerName,
                                new StringValues(headers[headerName].ToArray())
                            );

                    await (await msg.Render(new Body())).CopyToAsync(context.Response.Body);
                    context.Response.Body.Position = 0;
                    return context.Response;
                })
            )
        )
        { }
        
    }
}