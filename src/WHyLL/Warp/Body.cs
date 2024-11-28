namespace WHyLL.Warp;

/// <summary>
/// Renders the body of a message as <see cref="Stream"/>
/// </summary>
public sealed class Body() : WarpEnvelope<Stream>(
    new BodyAs<Stream>(body => body)
);

