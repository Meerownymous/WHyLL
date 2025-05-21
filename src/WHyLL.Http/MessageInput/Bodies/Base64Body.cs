using System.Text;
using Tonga.IO;
using Tonga.Text;
using WHyLL.Headers;
using WHyLL.Http.Headers;
using WHyLL.MessageInput;

namespace WHyLL.Http.MessageInput.Bodies;

/// <summary>
/// Puts a body encoded as base64 into http body.
/// </summary>
public sealed class Base64Body(Func<Stream> body) : MessageInputEnvelope(
    new JoinedInput(
        new HeaderInput(
            new ContentType("text/html; charset=utf-8")
        ),
        new HeaderInput(
            new Header("Content-Transfer-Encoding", "base64")
        ),
        new BodyInput(() =>
            new AsConduit(
                new TextAsBase64(
                    new AsText(new StreamReader(body(), Encoding.UTF8))
                ).AsString()
            ).Stream()
        )
    )
)
{
    public Base64Body(string body) : this(() => new AsConduit(body, Encoding.UTF8).Stream())
    { }
}