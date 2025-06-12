namespace WHyLL.Prologue;

/// <summary>
/// bunch of texts as prologue.
/// </summary>
public sealed class AsPrologue(string[] texts) : IPrologue
{
    public string[] Sequence() => texts;
}