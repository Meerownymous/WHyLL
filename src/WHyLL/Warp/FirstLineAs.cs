namespace WHyLL.Warp;

/// <summary>
/// Render the first line of a message as output type.
/// </summary>
public sealed class FirstLineAs<Output> : WarpEnvelope<Output>
{
    /// <summary>
    /// Render the first line of a message as output type.
    /// </summary>
    public FirstLineAs(Func<string, Output> render) : base(
        new PiecesAs<Output>((firstLine, y, z) => Task.FromResult(render(firstLine)))
    )
    { }

    /// <summary>
    /// Render the first line of a message as output type.
    /// </summary>
    public FirstLineAs(Func<string, Task<Output>> render) : base(
        new PiecesAs<Output>((firstLine,y,z) => render(firstLine))
    )
    { }
}

