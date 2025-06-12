using WHyLL.Prologue;

namespace WHyLL.MessageInput;

/// <summary>
/// Message input from firstline.
/// </summary>
public sealed class PrologueInput(IPrologue prologue) : MessageInputEnvelope(
    new SimpleMessageInput(
        msg => msg.With(prologue)
    )
)
{
    public PrologueInput(string[] words) : this(new AsPrologue(words))
    { }
}