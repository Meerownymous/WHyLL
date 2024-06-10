using System.Net;
using Tonga.Map;
using WHyLL.Message;

namespace WHyLL.AspNet.Response
{
    /// <summary>
    /// WHyLL message from asp response.
    /// </summary>
    public sealed class FromAspResponse : MessageEnvelope
    {
        /// <summary>
        /// WHyLL message from asp response.
        /// </summary>
        public FromAspResponse(Microsoft.AspNetCore.Http.HttpResponse response) : base(
            new Lambda(() =>
            {
                var msg =
                    new SimpleMessage()
                        .With(
                            $"HTTP/1.1 {response.StatusCode} {Enum.GetName(typeof(HttpStatusCode), response.StatusCode)}"
                        );

                foreach (var headerName in response.Headers.Keys)
                {
                    msg = msg.With(
                        Tonga.Enumerable.Mapped._(
                            value => AsPair._(headerName, value),
                            response.Headers[headerName]
                        )
                    );
                }
                return msg.WithBody(response.Body);
            })
        )
        { }
    }
}

