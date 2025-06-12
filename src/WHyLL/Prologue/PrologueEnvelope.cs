namespace WHyLL.Prologue;

/// <summary>
/// Envelope for prologues.
/// </summary>
public abstract class PrologueEnvelope(Func<string[]> origin) : IPrologue
{
    private readonly Lazy<string[]> texts = new(origin);
    
    /// <summary>
    /// Envelope for prologues.
    /// </summary>
    public PrologueEnvelope(IPrologue origin) : this(origin.Sequence)
    { }

    /// <summary>
    /// Envelope for prologues.
    /// </summary>
    public PrologueEnvelope(string[] origin) : this(() => origin)
    { }
    
    public string[] Sequence() => this.texts.Value;
}