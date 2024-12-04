namespace WHyLL.Warp;

/// <summary>
/// First header with the given name.
/// </summary>
public sealed class FirstHeader(string name) : WarpEnvelope<string>(
    new FirstHeaderAs<string>(name, header => header)
);

