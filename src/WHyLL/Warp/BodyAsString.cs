using System.Text;

namespace WHyLL.Warp;

/// <summary>
/// Renders the body of a message as <see cref="String"/>
/// </summary>
public sealed class BodyAsString(Encoding encoding) : WarpEnvelope<String>(
    new PiecesAs<string>(async (_, _, body) =>
    {
        var result = "";
        var originalPosition = 0L;
        if (body.CanSeek)
        {
            originalPosition = body.Position;
            body.Seek(0, SeekOrigin.Begin);
        }
        else if (body.Position != 0)
            throw new ArgumentException(
                "Cannot render body because stream position is not at 0 and cannot be moved. Consider activating replay via the AsAspResonse object.");
        using (var reader = new StreamReader(body, encoding, leaveOpen: true))
            result = await reader.ReadToEndAsync();
        
        if(body.CanSeek)
            body.Seek(originalPosition, SeekOrigin.Begin);

        return result;
    })
)
{
    /// <summary>
    /// Renders the body of a message as <see cref="String"/>
    /// </summary>
    public BodyAsString() : this(Encoding.UTF8)
    { }
}
