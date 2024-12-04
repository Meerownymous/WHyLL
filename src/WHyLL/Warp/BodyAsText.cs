using System.Text;

namespace WHyLL.Warp;

/// <summary>
/// Renders the body of a message as <see cref="String"/>
/// </summary>
public sealed class BodyAsText() : WarpEnvelope<String>(
    new PiecesAs<string>(async (_, _, body) =>
    {
        StringBuilder stringBuilder = new StringBuilder();
        byte[] buffer = new byte[4];
        int bytesRead;
        if (body.CanSeek)
            body.Seek(0, SeekOrigin.Begin);
        else if (body.Position != 0)
            throw new ArgumentException(
                "Cannot render body because stream position is not at 0 and cannot be moved. Consider activating replay via the AsAspResonse object.");
        while ((bytesRead = await body.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
        }

        return stringBuilder.ToString();
    })
);
