using Tonga;

namespace WHyLL.ByteWarp;

public abstract class ByteWarpEnvelope<TTarget>(IByteWarp<TTarget> origin) : IByteWarp<TTarget>
{
    public Task<TTarget> Render(byte[] head, IEnumerable<IPair<string, byte[]>> attributes, Stream body) =>
        origin.Render(head, attributes, body);
}