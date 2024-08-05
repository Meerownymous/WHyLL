using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using WHyLL.Http.Rendering;
using WHyLL.Rendering;

namespace WHyLL.AspNet.Rendering
{
    /// <summary>
    /// Renders a message as AspNetResponse into a HttpContext.
    /// </summary>
    public sealed class AspResponse(HttpContext context) : RenderingEnvelope<HttpResponse>(
        (MessageAs._(async msg =>
            {
                context.Response.StatusCode = await msg.Render(new StatusCode());
                var headers = await msg.Render(new AllHeaders());

                foreach (var headerName in headers.Keys())
                    context.Response.Headers
                        .Add(
                            headerName,
                            new StringValues(headers[headerName].ToArray())
                        );

                context.Response.Body = await msg.Render(new Body());
                return context.Response;
            })
        )
    )
    {
        /// <summary>
        /// Renders a message as AspNetResponse into a HttpContext.
        /// </summary>
        public AspResponse() : this(new DefaultHttpContext())
        { }
    }
}

