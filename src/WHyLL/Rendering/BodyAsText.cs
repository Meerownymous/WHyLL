using System.Text;
using Tonga.Text;

namespace WHyLL.Rendering
{
    /// <summary>
    /// Renders the body of a message as <see cref="String"/>
    /// </summary>
    public sealed class BodyAsText() : RenderingEnvelope<String>(
        new PiecesAs<string>(async (x, y, body) =>
            {
                StringBuilder stringBuilder = new StringBuilder();
                byte[] buffer = new byte[4];
                int bytesRead;
                while ((bytesRead = await body.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                }
                return stringBuilder.ToString();
            }
        )
    )
    { }
}

