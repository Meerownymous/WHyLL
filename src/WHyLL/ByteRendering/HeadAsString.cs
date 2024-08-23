using System.Text;

namespace WHyLL.ByteRendering;

public sealed class HeadAsString(Encoding enc) : ByteRenderingEnvelope<string>(
    new HeadAs<string>(head => Task.FromResult(enc.GetString(head)))
)
{
    public HeadAsString() : this(Encoding.UTF8){ }
}