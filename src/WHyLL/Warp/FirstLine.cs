namespace WHyLL.Warp;

/// <summary>
/// Render the first line of a request (request-line) or response (status-line)
/// </summary>
public sealed class FirstLine() : WarpEnvelope<string>(
    new FirstLineAs<string>(line => line)
);

