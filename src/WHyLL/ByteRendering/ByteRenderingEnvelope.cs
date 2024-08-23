using Tonga;

namespace WHyLL.ByteRendering;

public abstract class ByteRenderingEnvelope<TTarget>(IByteRendering<TTarget> origin) : IByteRendering<TTarget>
{
    public Task<TTarget> Render(byte[] head, IEnumerable<IPair<string, byte[]>> attributes, Stream body) =>
        origin.Render(head, attributes, body);
}