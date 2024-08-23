using Tonga;

namespace WHyLL;

public interface IByteRendering<TTarget>
{
    Task<TTarget> Render(byte[] head, IEnumerable<IPair<string, byte[]>> attributes, Stream body);
}