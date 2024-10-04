using System.Text;

namespace WHyLL.ByteWarp;

public sealed class HeadAsString(Encoding enc) : ByteWarpEnvelope<string>(
    new HeadAs<string>(head => Task.FromResult(enc.GetString(head)))
)
{
    public HeadAsString() : this(Encoding.UTF8){ }
}