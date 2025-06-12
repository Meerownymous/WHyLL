using Microsoft.AspNetCore.Http;
using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Message;
using WHyLL.Prologue;

namespace WHyLL.AspNet.Request
{
    /// <summary>
    /// WHyLL message from asp request.
    /// </summary>
    public sealed class UnwrapAspRequest(HttpRequest request, bool allowBodyReplay = false) : 
        MessageEnvelope(
            new Lambda(() =>
            {
                var msg =
                    new SimpleMessage()
                        .With(new AsPrologue([request.Method, request.Path, "HTTP/1.1"]));

                foreach(var headerName in request.Headers.Keys)
                {
                    msg = msg.With(
                        request
                            .Headers[headerName]
                            .AsMapped(value => (headerName, value).AsPair())
                    );
                }
                if(allowBodyReplay) request.EnableBuffering();
                return msg.WithBody(request.Body);
            })
        )
    { }
}