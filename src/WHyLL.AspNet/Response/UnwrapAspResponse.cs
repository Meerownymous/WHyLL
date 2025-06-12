using System.Net;
using Tonga.Enumerable;
using Tonga.Map;
using WHyLL.Message;
using WHyLL.Prologue;

namespace WHyLL.AspNet.Response
{
    /// <summary>
    /// WHyLL message from asp response.
    /// </summary>
    public sealed class UnwrapAspResponse(Microsoft.AspNetCore.Http.HttpResponse response) : 
        MessageEnvelope(
            new Lambda(() =>
            {
                var msg =
                    new SimpleMessage()
                        .With(
                            new AsPrologue(
                                [
                                    "HTTP/1.1",
                                    response.StatusCode.ToString(),
                                    Enum.GetName(typeof(HttpStatusCode), response.StatusCode)
                                ]
                            )
                        );

                foreach (var headerName in response.Headers.Keys)
                {
                    msg = msg.With(
                        response.Headers[headerName].AsMapped(value => (headerName, value).AsPair())
                    );
                }
                return msg.WithBody(response.Body);
            })
        )
    { }
}

