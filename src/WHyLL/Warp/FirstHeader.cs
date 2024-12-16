namespace WHyLL.Warp;

/// <summary>
/// First header with the given name.
/// </summary>
public sealed class FirstHeader(string name, Func<string> fallback) : WarpEnvelope<string>(
    new FirstHeaderAs<string>(name, header => header, fallback)
)
{
    /// <summary>
    /// First header with the given name.
    /// </summary>
    public FirstHeader(string name, string fallback) : this(name, () => fallback) { }
    
    /// <summary>
    /// First header with the given name.
    /// </summary>
    public FirstHeader(string name) : this(name, () => throw new ArgumentException($"Header {name} not found.")) { }
}

