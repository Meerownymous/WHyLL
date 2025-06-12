using System.Net;
using Tonga.Enumerable;
using WHyLL.Message;
using WHyLL.MessageInput;

namespace WHyLL.Http.Response;

/// <summary>
/// A http response with given statuscode and optional content.
/// </summary>
public sealed class HttpResponse(HttpStatusCode httpStatusCode, IEnumerable<IMessageInput> inputs) : MessageEnvelope(
    new MessageWithInputs(
        new JoinedInput(
            inputs,
            new MessageInput.ResponsePrologue(httpStatusCode)
        )
    )
)
{
    
    /// <summary>
    /// A http response with given statuscode and optional content.
    /// </summary>
    public HttpResponse(HttpStatusCode statusCode, params IMessageInput[] inputs) : this(
        statusCode,
        inputs.AsEnumerable()
    )
    { }
}