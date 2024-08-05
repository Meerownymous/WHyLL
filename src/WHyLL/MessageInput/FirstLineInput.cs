using Tonga;
using Tonga.Text;

namespace WHyLL.MessageInput;

/// <summary>
/// Message input from firstline.
/// </summary>
public sealed class FirstLineInput(IText firstLine) : MessageInputEnvelope(
    new SimpleMessageInput(
        msg => msg.With(firstLine.AsString())
    )
)
{
    /// <summary>
    /// Message input from firstline.
    /// </summary>
    public FirstLineInput(string firstLine) : this(AsText._(firstLine))
    { }
}