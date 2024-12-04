namespace WHyLL.Warp;

/// <summary>
/// First header with the given name as int.
/// </summary>
public sealed class FirstHeaderAsInt(string name) : WarpEnvelope<int>(
    new FirstHeaderAs<int>(name, Convert.ToInt32)
);