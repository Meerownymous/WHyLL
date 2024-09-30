using WHyLL.Warp;

namespace WHyLL.Http.Warp;

/// <summary>
/// Renders content-type header as string.
/// </summary>
public sealed class ContentType() : WarpEnvelope<string>(
    new FirstHeader("content-type")    
)
{ }