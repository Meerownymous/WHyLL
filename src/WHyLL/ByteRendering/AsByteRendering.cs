using Tonga;

namespace WHyLL.ByteRendering;

public sealed class AsByteRendering<TTarget>(Func<byte[], IEnumerable<IPair<string, byte[]>>, Stream, Task<TTarget>> render) : IByteRendering<TTarget>
{
    public AsByteRendering(Func<byte[], Task<TTarget>> render) : this(
        (head, _, _) => render(head)
    )
    { }
    
    public AsByteRendering(Func<IEnumerable<IPair<string,byte[]>>, Task<TTarget>> render) : this(
        (_, attributes, _) => render(attributes)
    )
    { }
    
    public AsByteRendering(Func<Stream, Task<TTarget>> render) : this(
        (_, _, body) => render(body)
    )
    { }
    
    public Task<TTarget> Render(byte[] head, IEnumerable<IPair<string, byte[]>> attributes, Stream body) =>
        render(head, attributes, body);
}