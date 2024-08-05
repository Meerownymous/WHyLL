using Tonga.Map;
using WHyLL.Message;

namespace WHyLL.AspNet.Request
{
    /// <summary>
    /// WHyLL message from asp request.
    /// </summary>
    public sealed class FromAspRequest(Microsoft.AspNetCore.Http.HttpRequest request) : 
        MessageEnvelope(
            new Lambda(() =>
            {
                var msg =
                    new SimpleMessage()
                        .With($"{request.Method} {request.Path} HTTP/1.1");

                foreach(var headerName in request.Headers.Keys)
                {
                    msg = msg.With(
                        Tonga.Enumerable.Mapped._(
                            value => AsPair._(headerName, value),
                            request.Headers[headerName]
                        )
                    );
                }
                return msg.WithBody(request.Body);
            })
        )
    { }
}

